using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverManager : MonoBehaviour {

	GameObject cannon;
	SimplePlayerHealth playerHealth;
	public float restartDelay = 5f;

	bool appeared = false;
	public Canvas gameOverCanvas;
	Text[] text;

	float restartTimer = 0f;

	// Use this for initialization
	void Start () {
		playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<SimplePlayerHealth> ();
		cannon = GameObject.FindGameObjectWithTag ("Cannon");
		text = gameOverCanvas.GetComponentsInChildren<Text>();
		gameOverCanvas.gameObject.SetActive (false);
		cannon.gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (playerHealth.isDead()) {
			if (!appeared) {
				gameOverCanvas.gameObject.SetActive (true);
				cannon.gameObject.SetActive (false);
				GameObject.FindGameObjectWithTag ("ScoreText").SetActive (false);
				GameObject.FindGameObjectWithTag ("HealthText").SetActive (false);
				text [1].text = "Score: " + GameObject.FindGameObjectWithTag ("Player").GetComponent<ScoreManager>().score;
				text [2].text = "Elapsed Time: " + (int)Time.timeSinceLevelLoad;
			}
			appeared = true;
			gameOverCanvas.gameObject.SetActive (true);
			cannon.gameObject.SetActive (false);
			restartTimer += Time.deltaTime;
			Debug.Log ("Restart in: " + (int)(restartDelay - restartTimer));
			text[0].text = "Restart in: " + (int)(restartDelay - restartTimer);
			if (restartTimer >= restartDelay) {
				RestartLevel ();
			}
		}
	}

	public void RestartLevel ()
	{
		Application.LoadLevel (Application.loadedLevel);
	}
}
