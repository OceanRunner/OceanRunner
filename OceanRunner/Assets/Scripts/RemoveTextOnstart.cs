using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveTextOnstart : MonoBehaviour {

	bool flag = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad >= Utils.calibrationTime && flag) {
			flag = false;
			gameObject.SetActive (false);
		}
	}
}
