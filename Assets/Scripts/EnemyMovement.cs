using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

	public int damagePerHit;
	public float timeBetweenHits;
	public float minDistanceToObject;
	float timer = 0;

	GameObject[] pylons;
	NavMeshAgent agent;
	int whichPylon;

	// Use this for initialization
	void Start () {
		pylons = GameObject.FindGameObjectsWithTag ("Pylon");
		agent = GetComponent<NavMeshAgent> ();
		whichPylon = Random.Range (0, pylons.Length);
		agent.destination = pylons [whichPylon].transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (!pylons[whichPylon]) {
			whichPylon = Random.Range (0, pylons.Length);
		}
		if (Vector3.Distance (gameObject.transform.position, pylons [whichPylon].transform.position) <= minDistanceToObject) {
			agent.Stop ();
			if (timer >= timeBetweenHits) {
				pylons [whichPylon].GetComponent<PylonHealth> ().TakeDamage (damagePerHit);
				timer = 0f;
			}
		}
	}
}
