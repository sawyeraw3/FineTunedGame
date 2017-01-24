using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveManager : MonoBehaviour {

	public static int wave;        // The player's score.
	public string playerID;
	
	Text text;                      // Reference to the Text component.

	void Awake ()
	{
		// Set up the reference.
		text = GameObject.Find(playerID + "WaveText").GetComponent <Text> ();
		
		// Reset the score.
		wave = 1;
	}
	
	
	void Update ()
	{
		// Set the displayed text to be the word "Score" followed by the score value.
		text.text = "Wave: " + wave;
	}
}
