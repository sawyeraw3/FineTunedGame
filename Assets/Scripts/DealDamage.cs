using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour {

	public float damageDealt;
	float delta;
	//public Color startCol;
	Color col;
	Renderer rend;
	public float deathDelay;

	// Use this for initialization
	void Start () {
		rend = gameObject.GetComponentInChildren<Renderer> ();
		//startCol = rend.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		delta = Time.deltaTime / deathDelay;
		//Debug.Log (deathDelay);
		if (damageDealt > 0) {
			damageDealt -= (delta * damageDealt);
		}

		col = rend.material.color;
		if ((col.a - delta) > 0f) {
			col.a -= delta;
			rend.material.color = col;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Enemy")) {
			float damage = damageDealt;
			Color otherCol = other.gameObject.transform.FindChild ("Colored").GetComponentInChildren<Renderer> ().material.color;
			if (otherCol.r == col.r && otherCol.g == col.g && otherCol.b == col.b) {
				damage *= 1.5f;
			} else {
<<<<<<< HEAD
				damage *= 0.3f;
=======
				damage *= .1f;
>>>>>>> origin/master
			}
			other.gameObject.GetComponent<EnemyHealth> ().TakeDamage (damage);
		}
		if (other.gameObject.layer == 0 || other.gameObject.layer == 2) {
			Destroy (transform.root.gameObject);
		}
	}
}
