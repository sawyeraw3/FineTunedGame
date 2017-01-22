using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

	public int damagePerHit;
	public float timeBetweenHits;
	float timer = 0;

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
		if (pylons [whichPylon]) {
			agent.destination = pylons [whichPylon].transform.position;
		} else {
			whichPylon = Random.Range (0, pylons.Length);
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Pylon") {
			agent.Stop();
			if (timer >= timeBetweenHits) {
				col.gameObject.GetComponent<PylonHealth> ().TakeDamage (damagePerHit);
			}
		}
	}

	void OnCollisionExit(Collision col) {
		
	}

}
