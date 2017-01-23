using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseManager : MonoBehaviour {


	bool isPaused = false;
	public Canvas pauseCanvas;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Start")) {
			isPaused = !isPaused;
			if (isPaused) {
				Time.timeScale = 0;
				pauseCanvas.gameObject.SetActive (isPaused);
			} else {
				pauseCanvas.gameObject.SetActive (isPaused);
				Time.timeScale = 1;
			}
		}
	}
}
