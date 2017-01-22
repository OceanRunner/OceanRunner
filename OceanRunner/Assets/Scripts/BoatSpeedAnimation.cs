using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSpeedAnimation : MonoBehaviour {

	public Sprite[] sprites;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float velx = GetComponent<Rigidbody2D> ().velocity.x;
		int frame = Mathf.Min (Mathf.Abs(Mathf.RoundToInt (velx * 1.5f)), sprites.Length - 1);
		GetComponent<SpriteRenderer> ().sprite = sprites [frame];
	}
}
