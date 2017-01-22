using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MicrophoneInput : MonoBehaviour {

	[SerializeField]
	private float multiplierStart = 50f;

	private AudioSource audio;
	private float[] chunk = new float[100];
	private float normalize = - 1f;
	private int normalizeLength = 44100*4;
	private float multiplier; // Mutliplier for force added goes to zero over time

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		audio.clip = Microphone.Start (Microphone.devices [0], true, 10, 44100);
		audio.loop = true;
		while (!(Microphone.GetPosition (null) > 0)) {
		} // Wait for mic to initialize
		//audio.Play();
		multiplier = multiplierStart;
	}
	
	// Update is called once per frame
	void Update () {
		audio.clip.GetData (chunk, Microphone.GetPosition(null) - chunk.Length);
		float sum = 0f;
		for( var i = 0; i < chunk.Length; i++) {
			sum += Mathf.Abs(chunk[i]);
		}
		float value = sum / chunk.Length;
		if (Time.timeSinceLevelLoad < Utils.calibrationTime) {
			if (normalize < 0) {
				normalize = value;
			} else {
				normalize = Mathf.Max (normalize, (normalize + value) / 2f);
			}
			return;
		} else if (value > normalize) {
			float force = multiplier * Mathf.Exp(value);
			//multiplier = Mathf.Max (multiplier - 0.5f, 0f);
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 ( force,0));
			//Debug.Log ("OK: " + force);
		} else {
			multiplier = multiplierStart;
		}
		//Debug.Log ("yeah: " + chunk[0]);
	}
}


