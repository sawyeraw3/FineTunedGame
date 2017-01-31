using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	  
	// Update is called once per frame
	void Update () {
		
	}

	public void loadLevel(int level) {
		SceneManager.LoadScene(level);
	}

	public void quitApp() {
		Application.Quit ();
	}

	//loads the menu level bitchhhhhh
	public void loadMainMenu(){
		Application.LoadLevel("MenuLevel");
	}
}
