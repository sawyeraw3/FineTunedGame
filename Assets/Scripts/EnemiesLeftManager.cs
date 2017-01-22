using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemiesLeftManager : MonoBehaviour {

	public static int enemiesKilled = 0;        // The player's score.
	public static int totalEnemies = 0;

	Text text;                      // Reference to the Text component.


	// Use this for initialization
	void Start () {
		// Set up the reference.
		text = GameObject.Find("EnemiesText").GetComponent <Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Enemies: " + (totalEnemies - enemiesKilled).ToString();
	}
}
