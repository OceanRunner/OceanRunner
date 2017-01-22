using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatGoRound : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x > Utils.getCameraBounds ().max.x) {
			transform.position = new Vector3(Utils.getCameraBounds ().min.x, transform.position.y, transform.position.z);
		}
	}
}
