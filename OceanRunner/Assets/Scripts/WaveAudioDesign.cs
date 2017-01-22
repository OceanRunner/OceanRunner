using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAudioDesign : MonoBehaviour {

	[SerializeField]
	private Material lineMaterial;

	[SerializeField]
	private GameObject coin;

	[SerializeField]
	private Transform goal;

	private LineRenderer lineRend;
	private Vector3 startPos;
	private Vector3 endPos;

	private bool playAudio = false;
	private AudioSource audioFile;
	// Use this for initialization
	void Start () {


		WWW www = new WWW("file://" + Utils.filename);

		audioFile = GetComponent<AudioSource>();
		audioFile.clip = www.audioClip;

		while (audioFile.clip.loadState != AudioDataLoadState.Loaded) {}

		//audioFile = GetComponent<AudioSource> ();

		float[] audiosamples = new float[audioFile.clip.samples * audioFile.clip.channels];

		Utils.songLength = audioFile.clip.length;

		// Set goal to end of song
		goal.position = new Vector3(Utils.songLength*Utils.gameSpeed, goal.position.y, goal.position.z);

		int runtime = (int)audioFile.clip.length;
		int i_sample = (int)(runtime * 4.0f /Utils.gameSpeed * Utils.freqency);
		int times = (int)(((audiosamples.Length) / 2) / i_sample);

		Vector3[] normalized_samples = new Vector3[i_sample];

		audioFile.clip.GetData (audiosamples, 0);

		int i = 0;
		int j = 0;
		int k = 0;
		while (k < i_sample) {
			if (i == j) {
				normalized_samples [k] = new Vector3 (Utils.gameSpeed*Utils.gameSpeed/4f*(float)k/Utils.freqency + transform.position.x, audiosamples [i],  transform.position.z);
				j = i + (times - 1);
				++k;
			}
			++i;
		}

		// some scaling
		float min = float.MaxValue, max = float.MinValue;
		for (i = 0; i < normalized_samples.Length; i++) {
			if (normalized_samples [i].y < min)
				min = normalized_samples [i].y;
			if (normalized_samples [i].y > max)
				max = normalized_samples [i].y;
		}

		// scale it to +/- 1
		for (i = 0; i < normalized_samples.Length; i++) {
			normalized_samples [i].y = ((normalized_samples [i].y - min) / (max - min) - 0.5f)*2*Utils.amplitude  + transform.position.y; // Plus adding the parent transform here
		}

		Vector3[] coins = Utils.MakeCoinPlacement (normalized_samples, 1);

		Utils.scoreMax = coins.Length;

		for (i = 0; i < coins.Length; i++) {
			GameObject newCoin = Instantiate (coin);
			newCoin.transform.parent = transform;
			newCoin.transform.position = coins [i];
		}

		Vector3[] smoothed = Utils.MakeSmoothCurve (normalized_samples, (int)(6));
		//Debug.Log("Normalize with: " +  (int)(12/Utils.gameSpeed/Utils.freqency));
		//Vector3[] smoothed = normalized_samples;
		smoothed [0].x -= 20;
		smoothed [smoothed.Length-1].x += 20;


		// Drawing Line
		k = smoothed.Length;
		// Setting the parameters of the line
		lineRend = gameObject.AddComponent<LineRenderer> ();
		lineRend.material = lineMaterial;
		lineRend.startWidth = 0.2F;
		lineRend.endWidth = 0.2F;


		lineRend.numPositions = k;
		lineRend.SetPositions (smoothed);

		//Generate colliders to follow the line 
		for (i = 0; i < (k - 1); i++) {

			startPos = smoothed [i];
			endPos = smoothed [i + 1];

			BoxCollider2D col = new GameObject ("Collider").AddComponent<BoxCollider2D> ();
			col.transform.parent = transform;
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
		if (Time.timeSinceLevelLoad < Utils.calibrationTime){
			return;
		} else if(playAudio == false){
			audioFile.volume = Utils.volume;
			audioFile.Play ();
			playAudio = true;
		}
		Camera.main.transform.position = 
			new Vector3 (Camera.main.transform.position.x + Time.deltaTime * Utils.gameSpeed, 
					     Camera.main.transform.position.y, 
						 Camera.main.transform.position.z);

	}

}


