using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour {

	public float damageDealt;
	float delta;
	Color col;
	Renderer rend;
	public float deathDelay;

	// Use this for initialization
	void Start () {
		rend = gameObject.GetComponentInChildren<Renderer> ();
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
		if (other.gameObject.layer == 0){
			Destroy (transform.root.gameObject);
		}
	}
}
