using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public float curHealth;
	bool dead;

	// Use this for initialization
	void Start () {
		dead = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (dead) {
			Destroy (gameObject);
			KillManager.kills += 1;
		}
	}

	public void TakeDamage(float damage) {
		if (!dead) {
			curHealth -= damage;
		}
		if (curHealth <= 0) {
			dead = true;
		}
	}
}
