using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseManager : MonoBehaviour {


	bool isPaused = false;
	GameObject player;
	public Canvas pauseCanvas;

	FirstPersonController fps;
	EmitRings eRings;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		player = GameObject.FindGameObjectWithTag ("Player");
		fps = player.GetComponent<FirstPersonController> ();
		eRings = player.GetComponent<EmitRings> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Start")) {
			isPaused = !isPaused;
			if (isPaused) {
				freezeControls (!isPaused);
				Time.timeScale = 0;
				pauseCanvas.gameObject.SetActive (isPaused);
			} else {
				freezeControls (!isPaused);
				pauseCanvas.gameObject.SetActive (isPaused);
				Time.timeScale = 1;
			}
		}
	}

	void freezeControls(bool b) {
		fps.enabled = b;
		eRings.enabled = b;
	}
}
