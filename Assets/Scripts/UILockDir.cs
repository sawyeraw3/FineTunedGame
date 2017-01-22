using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILockDir : MonoBehaviour {

	Quaternion rot;
	// Use this for initialization
	void Start () {
		Quaternion startRot = gameObject.transform.rotation;
		rot.Set(startRot.eulerAngles.x, 90f, startRot.eulerAngles.z, startRot.w);
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.rotation = rot;
	}
}
