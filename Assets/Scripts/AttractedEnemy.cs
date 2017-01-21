using UnityEngine;
using System.Collections;

public class AttractedEnemy : MonoBehaviour {

	GameObject player;
	public float AttractionStrength;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float masqr;
		Vector3 offset;
		offset = player.transform.position - transform.position;

		masqr = offset.sqrMagnitude;

		if (masqr > .0001f) {
			GetComponent<Rigidbody> ().AddForce ((AttractionStrength * offset.normalized / masqr)
			* GetComponent<Rigidbody> ().mass);
		}
	}
}
