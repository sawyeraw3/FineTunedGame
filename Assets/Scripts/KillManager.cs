using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KillManager : MonoBehaviour {
	
	public static int kills;        // The player's score.
	
	
	Text text;                      // Reference to the Text component.
	
	
	void Awake ()
	{
		// Set up the reference.
		text = GameObject.Find("KillText").GetComponent <Text> ();
		
		// Reset the score.
		kills = 0;
	}
	
	
	void Update ()
	{
		// Set the displayed text to be the word "Score" followed by the score value.
		text.text = "Kills: " + kills;
	}
}