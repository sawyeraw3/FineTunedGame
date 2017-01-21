using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour {

	public float damageDealt;
	float delta;
	Color col;
	Renderer rend;
	float deathDelay = 5f;

	// Use this for initialization
	void Start () {
		rend = gameObject.GetComponent<Renderer> ();
		Destroy (gameObject, deathDelay);
	}
	
	// Update is called once per frame
	void Update () {
		delta = Time.deltaTime / deathDelay;

		if (damageDealt > 0) {
			damageDealt -= (delta * damageDealt);
		}
			
		col = rend.material.color;
		if ((col.a - delta) > 0f) {
			col.a -= delta;
			rend.material.color = col;
		}
	}
}
