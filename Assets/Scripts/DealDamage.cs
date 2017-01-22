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
	GameManager gm;

	// Use this for initialization
	void Start () {
		rend = gameObject.GetComponentInChildren<Renderer> ();
		//startCol = rend.material.color;
		gm = transform.Find ("LevelManager").GetComponent<GameManager> ();
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
				damage *= CalcDamage(otherCol);
			}
			other.gameObject.GetComponent<EnemyHealth> ().TakeDamage (damage);
		}
		if (other.gameObject.layer == 0 || other.gameObject.layer == 2) {
			Destroy (transform.root.gameObject);
		}
	}

	float CalcDamage(Color c){
		float val1 = (c.r * 255f) + (c.g * 255f) + (c.b * 255f);
		float valLoc1;

		float val2 = (col.r * 255f) + (col.g * 255f) + (col.b * 255f);
		float valLoc2;

		for (int i = 0; i < ColorDif.ColDist.Length; i++) {
			if (val1 == ColorDif.ColDist [i])
				valLoc1 = i;
			if (val2 == ColorDif.ColDist [i])
				valLoc2 = i;
		}
		return ((ColorDif.ColDist.Length/10) 
					/ Mathf.Abs (valLoc1 - valLoc2));
	}

}
