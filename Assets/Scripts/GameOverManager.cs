using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOverManager : MonoBehaviour {

	public bool gameOver = false;
	public Canvas gameOverCanvas;
	public AudioClip[] gameOverSounds;
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
		if (!GameObject.FindWithTag("MasterPylon") && !gameOver) {
			player.GetComponent<FirstPersonController> ().enabled = false;//walkSpeed = 0;
			player.GetComponent<EmitRings> ().enabled = false;
			UICanvas.gameObject.SetActive (false);
			gameOverCanvas.gameObject.SetActive (true);
			GameObject.Find ("KillText").GetComponent<Text> ().text = text [1].text;
			GameObject.Find ("WaveText").GetComponent<Text> ().text = text [2].text;
			AudioSource sound = gameObject.GetComponentInChildren<AudioSource> ();
			sound.clip = gameOverSounds [Random.Range (0, gameOverSounds.Length)];
			sound.volume = 1;
			sound.Play ();
			gameOver = true;
			/*restartTimer += Time.deltaTime;
			Debug.Log ("Restart in: " + (int)(restartDelay - restartTimer));
			text[0].text = "Restart in: " + (int)(restartDelay - restartTimer);
			if (restartTimer >= restartDelay) {
				RestartLevel ();
			}*/
		}
	}

	public void loadLevel(int level) {
		SceneManager.LoadScene(level);
	}

	/*
	public void restartLevel() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		Debug.Log ("restart");
	}

	public void loadMenuLevel() {
		SceneManager.LoadScene("MenuLevel");
	}*/
}
