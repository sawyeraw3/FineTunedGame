using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour {

	public float moveSpeed = 5.0f;

	public float frequency = 20.0f;
	public float magnitude = 0.5f;
	GameObject player;
	Vector3 axis;
	Vector3 pos;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		pos = transform.position;

		axis = transform.up;
	}
	
	// Update is called once per frame
	void Update () {
		pos += Time.deltaTime * transform.forward * moveSpeed;
		transform.position = pos + axis * Mathf.Sin (Time.time * frequency) * magnitude;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == 9){
			Destroy (gameObject);
		}
	}
}
