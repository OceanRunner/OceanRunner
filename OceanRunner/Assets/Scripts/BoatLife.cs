using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatLife : MonoBehaviour {

	public int life = 3;

	// Use this for initialization
	void Start () {
		
	}

	void ResetBoat () {
		transform.position = new Vector3 (Camera.main.transform.position.x, -1f, -2f);
		transform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, 0f));
		GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		GetComponent<Rigidbody2D> ().angularVelocity = 0;

	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < Utils.getCameraBounds().min.x || transform.position.x > Utils.getCameraBounds().max.x ||
			(transform.rotation.eulerAngles.z < -100 && transform.rotation.eulerAngles.z > -180) || 
			(transform.rotation.eulerAngles.z > 100 && transform.rotation.eulerAngles.z < 260) ) {
			life--;
			ResetBoat ();
		} 
	}
}

