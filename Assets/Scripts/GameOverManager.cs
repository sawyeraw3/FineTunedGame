using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOverManager : MonoBehaviour {

	public bool gameOver = false;
	public GameObject gameOverCanvas;
	public AudioClip[] gameOverSounds;
	GameObject player;
	GameObject UICanvas;
	GameObject masterP;


	//Freq, then Kills, then Wave
	Text[] text;

	PlayerController fps;
	EmitRings eRings;
	GameObject newButton;
	/*
	GameObject cannon;
	SimplePlayerHealth playerHealth;
	public float restartDelay = 5f;

	float restartTimer = 0f;
	*/

	// Use this for initialization
	void Start () {
		newButton = gameOverCanvas.transform.FindChild ("RestartButton").gameObject;

		player = GameObject.FindGameObjectWithTag ("Player");
		fps = player.GetComponent<PlayerController> ();
		eRings = player.GetComponent<EmitRings> ();
		UICanvas = GameObject.Find ("UICanvas");
		text = UICanvas.GetComponentsInChildren<Text>();
		gameOverCanvas.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameObject.FindWithTag("MasterPylon") && !gameOver) {
			fps.enabled = !fps.enabled;
			eRings.enabled = !eRings.enabled;
			EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(newButton);
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
}
