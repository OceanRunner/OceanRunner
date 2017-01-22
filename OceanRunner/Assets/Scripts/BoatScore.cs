using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatScore : MonoBehaviour {

	public int score = 0;
	public Text scoreLabel;

	// Use this for initialization
	void Start () {
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Coin") {
			score++;
			scoreLabel.text = "Score: " + score;
			Destroy (other.gameObject);
		}
	}
}
