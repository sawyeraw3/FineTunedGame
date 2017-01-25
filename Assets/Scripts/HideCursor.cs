using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCursor : MonoBehaviour {

	public bool hideCursor;

	// Use this for initialization
	void Start () {
		Cursor.visible = !hideCursor;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
