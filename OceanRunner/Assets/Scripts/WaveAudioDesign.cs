﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAudioDesign : MonoBehaviour {


	// Use this for initialization
	void Start () {
		Debug.Log ("here");	
		AudioSource audioFile= GetComponent<AudioSource>();

		float[] audiosamples = new float[audioFile.clip.samples * audioFile.clip.channels];
		Debug.Log (audioFile.clip.channels);

		int runtime = (int) audioFile.clip.length;
		int i_sample = runtime * 2;
		int times = (int) (((audiosamples.Length) / 2) / i_sample);

		Vector3[] normalized_samples = new Vector3[i_sample];

		audioFile.clip.GetData(audiosamples, 0);

		int i = 0;
		int j = 0;
		int k = 0;
		while (k < i_sample) {
			if (i == j) {
				normalized_samples[k] = new Vector3(k, (audiosamples [i] * 1F),0F);
				j = i + (times-1);
				++k;
			}
			++i;
		}

		normalized_samples = Curver.MakeSmoothCurve (normalized_samples, 3.0F);
		k = normalized_samples.Length;

		// visualize points with cubes on screen.
		Debug.Log (k);
		for (i=0; i<k; i++){
			GameObject box = GameObject.CreatePrimitive (PrimitiveType.Cube);
			box.transform.position = normalized_samples[i] ;
			box.transform.localScale = new Vector3 (0.1F,0.1F,0.1F);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
}


