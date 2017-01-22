using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVars : MonoBehaviour {

	public GameObject[] ps;
	public List<GameObject> pylons = new List<GameObject>();
	public bool allPylonsDestroyed;

	// Use this for initialization
	void Start () {
		allPylonsDestroyed = false;

		ps = GameObject.FindGameObjectsWithTag ("Pylon");

		foreach (GameObject go in ps) {
			pylons.Add (go);
		}
	}
	
	// Update is called once per frame
	void Update () {
		ps = GameObject.FindGameObjectsWithTag ("Pylon");
		if (ps.Length == 0)
			allPylonsDestroyed = true;
	}
}
