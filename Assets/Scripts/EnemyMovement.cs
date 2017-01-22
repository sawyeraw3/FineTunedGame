using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

	GameObject[] pylons;
	NavMeshAgent agent;
	int whichPylon;

	// Use this for initialization
	void Start () {
		pylons = GameObject.FindGameObjectsWithTag ("Pylon");
		agent = GetComponent<NavMeshAgent> ();
		whichPylon = Random.Range (0, pylons.Length);
	}
	
	// Update is called once per frame
	void Update () {
		agent.destination = pylons[whichPylon].transform.position;
	}
}
