using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemiesLeftManager : MonoBehaviour {

	public string playerID;
	public static int enemiesLeft;        // The player's score.
	public static int totalEnemies;

	Text text;                      // Reference to the Text component.

	// Use this for initialization
	void Awake () {
		// Set up the reference.
		text = GameObject.Find(playerID + "EnemiesText").GetComponent <Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Enemies: " + totalEnemies;
	}
}