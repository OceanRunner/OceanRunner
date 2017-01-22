using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandGenerator : MonoBehaviour {

	GameObject[] islands;
	enum island_type {Island1, Island2, Island3, Island4, Island5}; 
	int rnd_select;
	string rnd_name;

	[SerializeField]
	private float speed = 0.5f;

	[SerializeField]
	private int numberOfIslands = 2;

	void CreateNewIsland(int i){
		// Select clouds randomly
		rnd_select = Random.Range (0, 5);
		island_type cloudType = (island_type)rnd_select;
		rnd_name = cloudType.ToString();

		islands[i] = Instantiate(Resources.Load(rnd_name, typeof(GameObject))) as GameObject;		

		float rnd_x = Random.Range (22f + i*35f/numberOfIslands, 22f + (i+1)*35f/numberOfIslands);
			
		islands[i].transform.parent = transform;
		islands[i].transform.localPosition = new Vector3 (rnd_x, 0, 0);

	}

	// Use this for initialization
	void Start () {
		islands = new GameObject[numberOfIslands];
		for (int i = 0; i < numberOfIslands; i++) {	
			CreateNewIsland (i);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad < Utils.calibrationTime)
			return;
		for (int i = 0; i < numberOfIslands; i++) {
			islands[i].transform.Translate (Vector3.left * Time.deltaTime * speed, Space.World); // needed to be modified depends on velocity of the boat
			if (islands[i].transform.localPosition.x < -22) {
				Destroy (islands[i]);
				CreateNewIsland (i);
			}
		}
	}
}
