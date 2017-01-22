using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour {

	[SerializeField]
	private float speed = 0.2f;

	[SerializeField]
	private int numberOfClouds = 4;

	GameObject[] clouds;
	enum cloud_type {Cloud1, Cloud2, Cloud3, Cloud4}; 
	int rnd_cloud_i;
	float rnd_x;
	float rnd_y;
	string rnd_cloud;

//	float shrinkSpeed = 0.5f;
	int tick  = 0;

	void CreateCloud(int i){
		// Select clouds randomly
		rnd_cloud_i = Random.Range (0, 3);
		cloud_type cloudType = (cloud_type)rnd_cloud_i;
		rnd_cloud = cloudType.ToString();

		clouds[i] = Instantiate(Resources.Load(rnd_cloud, typeof(GameObject))) as GameObject;	
		rnd_x = Random.Range (16f + i*35f/numberOfClouds, 16f + (i+1)*35f/numberOfClouds);
		rnd_y = Random.Range (2f, 5f) + Camera.main.transform.position.y;

		clouds [i].transform.parent = transform;
		clouds[i].transform.localPosition = new Vector3 (rnd_x, rnd_y, 0);

	}

	// Use this for initialization
	void Start () { 
		clouds = new GameObject[numberOfClouds];
		for (int i = 0; i < numberOfClouds; i++) {
			CreateCloud(i);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad < Utils.calibrationTime)
			return;
		for (int i = 0; i < numberOfClouds; i++) {
			clouds[i].transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);

			if (clouds[i].transform.localPosition.x < -16) { // end of the line
				Destroy(clouds[i]);
				CreateCloud (i);
			}
		}
			
//		cloud.transform.localScale -= Vector3.one*Time.deltaTime*shrinkSpeed;
//		cloud.transform.localScale += Vector3.one*Time.deltaTime*shrinkSpeed;

		tick++;

	}
}


// 