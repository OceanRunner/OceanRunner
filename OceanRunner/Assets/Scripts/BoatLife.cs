using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatLife : MonoBehaviour {

	public GameObject lifeObj;
	public GameObject scoreLabel;

	public int life = 3;

	private GameObject[] lifeObjs;
	// Use this for initialization
	void Start () {
		lifeObjs = new GameObject[life];
		for (int i = 0; i < life; i++) {
			lifeObjs [i] = Instantiate (lifeObj);
			lifeObjs [i].transform.parent = Camera.main.transform;
			lifeObjs [i].transform.position = new Vector3 (Utils.getCameraBounds().max.x - (i+1)*1.3f, Utils.getCameraBounds().max.y - 1f, -5f);
		}
		RectTransform rect = scoreLabel.GetComponent<RectTransform> ();
		RectTransform parentRect = scoreLabel.transform.parent.gameObject.GetComponent<RectTransform> ();
		rect.transform.position = new Vector3(-parentRect.rect.width / 2 + 450, parentRect.rect.height/2f-80, 0f);
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
			(transform.rotation.eulerAngles.z < -140 && transform.rotation.eulerAngles.z > -180) || 
			(transform.rotation.eulerAngles.z > 140 && transform.rotation.eulerAngles.z < 220) ) {
			life--;
			if (life >= 0) {
				lifeObjs [life].SetActive (false);
			}
			ResetBoat ();
		} 
	}
}

