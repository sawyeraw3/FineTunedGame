using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHit : MonoBehaviour {

	FirstPersonController controller;
	EmitRings emitter;
	public AudioClip stun;
	BoxCollider collide;
	SphereCollider collide1;

	// Use this for initialization
	void Start () {
		controller = gameObject.GetComponent<FirstPersonController> ();
		emitter = gameObject.GetComponent<EmitRings> ();
		collide = gameObject.GetComponentInChildren<BoxCollider> ();
		collide1 = gameObject.GetComponentInChildren<SphereCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Enemy") {
			Vector3 d = gameObject.transform.position - other.gameObject.transform.position;
			IEnumerator coroutine = PlayerHit (d);
			StartCoroutine (coroutine);
		}
	}

	IEnumerator PlayerHit(Vector3 dir) {
		AudioSource sound = gameObject.GetComponentInChildren<AudioSource> ();
		controller.enabled = false;
		emitter.enabled = false;
		collide.gameObject.layer = 10;
		collide1.gameObject.layer = 10;
		Rigidbody r = GetComponent<Rigidbody> ();
		dir.y = 15;
		r.AddForce (Vector3.Scale(dir, new Vector3(30, 1, 30)), ForceMode.Impulse);
		sound.clip = stun;
		sound.Play ();
		yield return new WaitForSeconds (stun.length);
		controller.enabled = true;
		emitter.enabled = true;
		collide.gameObject.layer = 8;
		collide1.gameObject.layer = 8;
	}
}
