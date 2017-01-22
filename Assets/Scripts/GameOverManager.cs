using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOverManager : MonoBehaviour {

	bool appeared = false;
	public Canvas gameOverCanvas;
	GameObject player;
	GameObject UICanvas;
	GameObject masterP;
	//Freq, then Kills, then Wave
	Text[] text;
	/*
	GameObject cannon;
	SimplePlayerHealth playerHealth;
	public float restartDelay = 5f;

	float restartTimer = 0f;
	*/

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		UICanvas = GameObject.Find ("UICanvas");
		text = UICanvas.GetComponentsInChildren<Text>();
		gameOverCanvas.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameObject.FindWithTag("MasterPylon") && !appeared) {
			if (!appeared) {
				player.GetComponent<FirstPersonController> ().enabled = false;//walkSpeed = 0;
				player.GetComponent<EmitRings> ().enabled = false;
				UICanvas.gameObject.SetActive (false);
				gameOverCanvas.gameObject.SetActive (true);
				GameObject.Find ("KillText").GetComponent<Text> ().text = text [1].text;
				GameObject.Find ("WaveText").GetComponent<Text> ().text = text [2].text;
				appeared = true;
			}
		}
	}
}
