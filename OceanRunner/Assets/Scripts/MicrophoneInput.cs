using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MicrophoneInput : MonoBehaviour {

	private AudioSource audio;
	private float[] chunk = new float[100];
	private float normalize = - 1f;
	private int normalizeLength = 44100*4;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		audio.clip = Microphone.Start (Microphone.devices [0], true, 10, 44100);
		audio.loop = true;
		while (!(Microphone.GetPosition (null) > 0)) {
		} // Wait for mic to initialize
		//audio.Play();
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
			float force = Utils.sensitivity * Mathf.Exp(value);
			//multiplier = Mathf.Max (multiplier - 0.5f, 0f);
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 ( force,0));
			//Debug.Log ("OK: " + force);
		}
		//Debug.Log ("yeah: " + chunk[0]);#


		if (transform.position.x > Utils.songLength * Utils.gameSpeed) {
			// We are winning!
			Utils.score = GetComponent<BoatScore>().score;
			SceneManager.LoadScene ("Win");
		}
	}
}


