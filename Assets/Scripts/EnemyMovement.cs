using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

	public int damagePerHit;
	public float timeBetweenHits;
	public float minDistanceToObject;
	float minDistanceToMaster;
	float minDistanceTemp;
	public AudioClip attack;
	public AudioClip destroy;

	float timer = 0;

	GameObject target;
	GameObject prevTarget;
	NavMeshAgent agent;
	int whichPylon;

	EnemyVars vars;

	// Use this for initialization
	void Start () {
		minDistanceToMaster = minDistanceToObject * 1.5f;
		vars = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<EnemyVars> ();
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
				target = target.gameObject;
				agent.destination = target.transform.position;
				if (Vector3.Distance (gameObject.transform.position, target.transform.position) <= minDistanceToObject) {
					agent.Stop ();
					if (target != GameObject.Find ("Player") && timer >= timeBetweenHits) {
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
			} else {
				whichPylon = Random.Range (0, vars.pylons.Count);
				target = vars.pylons [whichPylon];
				agent.destination = target.transform.position;
				agent.Resume ();
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
										
											// you guys should add more comments
									
			}                                                                 
		} else {
			agent.Stop();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player" && (target != null) && (transform.position - target.transform.position).magnitude > 15) {
			minDistanceTemp = minDistanceToObject;
			minDistanceToObject = 0;
			agent.stoppingDistance = 0;
			agent.angularSpeed = 270;
			agent.speed = 5f;
			prevTarget = target;
			target = other.gameObject;
			agent.destination = target.transform.position;
		}
	}
	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player" && target == other.gameObject) {
			agent.angularSpeed = 90;
			agent.speed = 3.5f;
			target = prevTarget;
			minDistanceToObject = minDistanceTemp;
			agent.destination = target.transform.position;
		}
	}
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Player" && target == other.gameObject) {
			agent.angularSpeed = 90;
			agent.speed = 3.5f;
			target = prevTarget;
			minDistanceToObject = minDistanceTemp;
			agent.destination = target.transform.position;
		}
	}
}
