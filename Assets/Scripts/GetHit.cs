using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHit : MonoBehaviour {

	PlayerController controller;
	EmitRings emitter;
	public AudioClip stun;
	BoxCollider collide;
	SphereCollider collide1;

	// Use this for initialization
	void Start () {
		controller = gameObject.GetComponent<PlayerController> ();
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
		Renderer[] rends = gameObject.GetComponentsInChildren<Renderer> ();
		float nTimes = Mathf.Round(stun.length)*4;
		while (nTimes > 0) {
			foreach(Renderer re in rends)
				re.enabled = true;
			yield return new WaitForSeconds (0.125f);
			foreach(Renderer re in rends)
				re.enabled = false;
			yield return new WaitForSeconds (0.125f);
			nTimes--;
		}
		foreach(Renderer re in rends)
			re.enabled = true;
		controller.enabled = true;
		emitter.enabled = true;
		yield return new WaitForSeconds (1f);
		collide.gameObject.layer = 8;
		collide1.gameObject.layer = 8;
	}
}
