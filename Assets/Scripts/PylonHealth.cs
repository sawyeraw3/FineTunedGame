using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonHealth : MonoBehaviour {

	public int curHealth;
	public bool isDestroyed;

	// Use this for initialization
	void Start () {
		isDestroyed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isDestroyed)
			Destroy(gameObject);
	}

	public void TakeDamage (int damage) {
		if (curHealth > 0) {
			curHealth -= damage;
		} else {
			isDestroyed = true;
		}
	}
}
