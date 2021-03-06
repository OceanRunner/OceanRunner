﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour {

	public InputField fileInput;
	public Slider volume;
	public Slider amplitude;
	public Slider samplerate;
	public Slider speed;
	public Slider sensitivity;


	// Use this for initialization
	void Start () {
		fileInput.text = Utils.filename;
		volume.value = Utils.volume;
		amplitude.value = Utils.amplitude;
		samplerate.value = Utils.freqency;
		speed.value = Utils.gameSpeed;
		sensitivity.value = Utils.sensitivity;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void saveSettings() {
		Utils.filename = fileInput.text;
		Utils.volume = volume.value;
		Utils.amplitude = amplitude.value;
		Utils.freqency = samplerate.value;
		Utils.gameSpeed = speed.value;
		Utils.sensitivity = sensitivity.value;
		SceneManager.LoadScene ("Menu");
	}

	public void changeSens() {
		Utils.sensitivity = sensitivity.value;
	}

	void loadFileToField (string path)
	{
		fileInput.text = path;
	}

	public void loadFile() {

		GetComponent<Browser> ().OpenFile (Application.dataPath);
	}
}
