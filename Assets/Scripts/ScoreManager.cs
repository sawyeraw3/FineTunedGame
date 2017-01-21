using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public int score = 0;
	Text text;

	// Use this for initialization
	void Start () {
		text = GameObject.FindGameObjectWithTag ("ScoreText").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Score: " + score;
	}
}
