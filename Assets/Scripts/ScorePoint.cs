using UnityEngine;
using System.Collections;

public class ScorePoint : MonoBehaviour {

	ScoreManager scoreManager;
	string myTag;

	// Use this for initialization
	void Start () {
		scoreManager = GameObject.FindGameObjectWithTag ("Player").GetComponent<ScoreManager> ();
		myTag = gameObject.tag;
		myTag += "G";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col) {
		if(col.gameObject.tag == myTag){
			scoreManager.score += 1;
			gameObject.SetActive (false);
		}
	}
}
