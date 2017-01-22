using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class islandGenerator : MonoBehaviour {

	GameObject island;
	enum island_type {Island1, Island2, Island3, Island4, Island5, Island6}; 
	int rnd_select;
	string rnd_name;

	void CreateIsland(){
		// Select clouds randomly
		rnd_select = Random.Range (0, 6);
		island_type cloudType = (island_type)rnd_select;
		rnd_name = cloudType.ToString();

		island = Instantiate(Resources.Load(rnd_name, typeof(GameObject))) as GameObject;		
		island.transform.position = new Vector3 (16, -2, 30);

	}

	// Use this for initialization
	void Start () {
		CreateIsland();
	}
	
	// Update is called once per frame
	void Update () {
		island.transform.Translate(Vector3.left * Time.deltaTime * 5, Space.World); // needed to be modified depends on velocity of the boat
		if (island.transform.position.x < -20)
			CreateIsland();
	}
}
