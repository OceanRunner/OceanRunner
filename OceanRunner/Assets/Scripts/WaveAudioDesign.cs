using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAudioDesign : MonoBehaviour {

	private LineRenderer lineRend;
	private Vector3 startPos;
	private Vector3 endPos;

	private bool playAudio = false;
	private AudioSource audioFile;
	// Use this for initialization
	void Start () {
		audioFile = GetComponent<AudioSource> ();

		float[] audiosamples = new float[audioFile.clip.samples * audioFile.clip.channels];

		int runtime = (int)audioFile.clip.length;
		int i_sample = runtime * 2;
		int times = (int)(((audiosamples.Length) / 2) / i_sample);

		Vector3[] normalized_samples = new Vector3[i_sample];

		audioFile.clip.GetData (audiosamples, 0);

		int i = 0;
		int j = 0;
		int k = 0;
		while (k < i_sample) {
			if (i == j) {
				normalized_samples [k] = new Vector3 (k, (audiosamples [i] * 1F), 0f);
				j = i + (times - 1);
				++k;
			}
			++i;
		}

		Vector3[] smoothed = Curver.MakeSmoothCurve (normalized_samples, 6);

		Color lineColour = Color.blue;

		k = smoothed.Length;
		// Setting the parameters of the line
		lineRend = gameObject.AddComponent<LineRenderer> ();
		lineRend.material = new Material (Shader.Find ("Diffuse"));
		lineRend.startColor = lineColour;
		lineRend.endColor = lineColour;
		lineRend.startWidth = 0.2F;
		lineRend.endWidth = 0.2F;


		lineRend.numPositions = k;
		lineRend.SetPositions (smoothed);

		//Generate colliders to follow the line 
		for (i = 0; i < (k - 1); i++) {

			startPos = smoothed [i];
			endPos = smoothed [i + 1];

			BoxCollider2D col = new GameObject ("Collider").AddComponent<BoxCollider2D> ();

			float lineLength = Vector3.Distance (startPos, endPos); // length of line
			col.size = new Vector3 (lineLength, 0.1f, 1f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
			Vector3 midPoint = (startPos + endPos) / 2;
			col.transform.position = midPoint; // setting position of collider object
			// Following lines calculate the angle between startPos and endPos
			col.sharedMaterial = (PhysicsMaterial2D)Resources.Load("PhysicsMaterial2D/Slippy");
			float angle = (Mathf.Abs (startPos.y - endPos.y) / Mathf.Abs (startPos.x - endPos.x));
			if ((startPos.y < endPos.y && startPos.x > endPos.x) || (endPos.y < startPos.y && endPos.x > startPos.x)) {
				angle *= -1;
			}
			angle = Mathf.Rad2Deg * Mathf.Atan (angle);
			if (!float.IsNaN (angle) && !float.IsInfinity (angle))
				col.transform.Rotate (0, 0, angle);
			else
				col.transform.Rotate (0, 0, 90F);
			
			

		}
	}
		
	// Update is called once per frame
	void Update () {
		if (Time.realtimeSinceStartup < 4) {
			return;
		} else if(playAudio == false){
			audioFile.Play ();
			playAudio = true;
		}
		Camera.main.transform.position = 
			new Vector3 (Camera.main.transform.position.x + Time.deltaTime * 2, 
					     Camera.main.transform.position.y, 
						 Camera.main.transform.position.z);

	}

}


