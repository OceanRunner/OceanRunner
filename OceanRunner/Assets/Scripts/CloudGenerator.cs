using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour {

	[SerializeField]
	private float speed = 3;

	[SerializeField]
	private int numberOfClouds = 4;

	GameObject[] cloud;
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

		cloud[i] = Instantiate(Resources.Load(rnd_cloud, typeof(GameObject))) as GameObject;	
		rnd_x = Random.Range (11, 35) + Camera.main.transform.position.x;
		rnd_y = Random.Range (2, 5) + Camera.main.transform.position.y;

		cloud[i].transform.position = new Vector3 (rnd_x, rnd_y, 50);

	}

	// Use this for initialization
	void Start () { 
		cloud = new GameObject[numberOfClouds];
		for (int i = 0; i < numberOfClouds; i++) {
			CreateCloud(i);
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < numberOfClouds; i++) {
			Debug.Log (i + "; " + speed);
			cloud[i].transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);

			if (cloud[i].transform.position.x < -12 + Camera.main.transform.position.x) { // end of the line
				CreateCloud (i);
			}
		}
			
//		cloud.transform.localScale -= Vector3.one*Time.deltaTime*shrinkSpeed;
//		cloud.transform.localScale += Vector3.one*Time.deltaTime*shrinkSpeed;

		tick++;

	}
}


// 