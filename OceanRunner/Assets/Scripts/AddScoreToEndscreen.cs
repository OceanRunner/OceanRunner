﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddScoreToEndscreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Text> ().text = "You scored: " + Utils.score + " out of " + Utils.scoreMax;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
