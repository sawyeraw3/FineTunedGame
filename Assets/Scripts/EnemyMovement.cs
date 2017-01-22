using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

	public int damagePerHit;
	public float timeBetweenHits;
	public float minDistanceToObject;
	float minDistanceToMaster;
	public AudioClip attack;
	public AudioClip destroy;

	float timer = 0;

	GameObject target;
	NavMeshAgent agent;
	int whichPylon;

	EnemyVars vars;

	// Use this for initialization
	void Start () {
		minDistanceToMaster = minDistanceToObject * 1.5f;
		vars = GameObject.Find("LevelManager").GetComponent<EnemyVars> ();
		agent = GetComponent<NavMeshAgent> ();

		if (vars.pylons.Count > 0) {
			whichPylon = Random.Range (0, vars.pylons.Count);
			target = vars.pylons [whichPylon];
			agent.destination = target.transform.position;
		}
	}
	
	// Update is called once per frame
	void Update () {
		AudioSource sound = GetComponentInChildren<AudioSource> ();
		timer += Time.deltaTime;

		if (!vars.allPylonsDestroyed) {
			if (target != null) {
				if (Vector3.Distance (gameObject.transform.position, target.transform.position) <= minDistanceToObject) {
					agent.Stop ();
					if (target != null && timer >= timeBetweenHits) {
						target.GetComponentInParent<PylonHealth> ().TakeDamage (damagePerHit);
						sound.clip = attack;
						sound.volume = 1;
						sound.Play ();
						timer = 0f;
					}
				}
				if (target.GetComponent<PylonHealth> ()) {
					if (target.GetComponent<PylonHealth> ().isDestroyed) {
						vars.pylons.Remove (target.gameObject);
						Destroy (target.gameObject);
						sound.clip = destroy;
						sound.volume = 1;
						sound.Play ();
						if (vars.pylons.Count != 0) {
							whichPylon = Random.Range (0, vars.pylons.Count);
							target = vars.pylons [whichPylon];
							if (target != null) {
								agent.destination = target.transform.position;
								agent.Resume ();
							}
						}
					}
				}
			}
		} else if (GameObject.FindGameObjectWithTag ("MasterPylon")) {
			minDistanceToObject = minDistanceToMaster;
			target = GameObject.FindGameObjectWithTag ("MasterPylon");
			agent.destination = target.transform.position;
			agent.Resume ();

			if (Vector3.Distance (gameObject.transform.position, target.transform.position) <= minDistanceToObject) {
				agent.Stop ();
				if (target.GetComponent<PylonHealth> ().isDestroyed) {
					
					Destroy (target.transform.root.gameObject);
					agent.Stop ();
				} else if (target != null && timer >= timeBetweenHits) {
					target.GetComponentInParent<PylonHealth> ().TakeDamage (damagePerHit);
					sound.clip = attack;
					sound.volume = 1;
					sound.Play ();
					timer = 0f;
				}
			}
		} else {
			agent.Stop();
		}
	}
}
