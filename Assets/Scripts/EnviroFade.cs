using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviroFade : MonoBehaviour {

	public string playerID;
	bool blocked;
	GameObject player;
	Camera cam;
	RaycastHit hitInfo;
	GameObject blocking;

	void Start () {
		blocked = false;
		cam = GameObject.Find(playerID + "Camera").GetComponent<Camera>();
		player = GameObject.Find (playerID);
	}
	
	// Update is called once per frame
	void Update () {
		if (blocked) {
			Component[] children;
			children = blocking.GetComponentsInChildren<Renderer> ();
			int i = 0;
			foreach (Renderer r in children)
			{
				Color col = r.material.color;
				col.a = 0.5f;
				r.material.color = col;

			}
		} else if (!blocked && blocking) {
			foreach (Renderer r in blocking.GetComponentsInChildren<Renderer>())
			{
				Color col = r.material.color;
				col.a = 1;
				r.material.color = col;

			}
		}

	}

	void FixedUpdate() {
		LayerMask mask = (1 << 0);
		Vector3 dir = player.transform.position - cam.transform.position;
		float distance = dir.magnitude - 1;
		if (Physics.Raycast (cam.transform.position, dir, out hitInfo, distance, mask)) {
			blocked = true;
			blocking = hitInfo.collider.gameObject;
		} else {
			blocked = false;
		}
	}
}
