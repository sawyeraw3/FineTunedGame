using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimplePlayerHealth : MonoBehaviour {
	
	public int health;
	bool dead;
	Text text;

	// Use this for initialization
	void Start () {
		text = GameObject.FindGameObjectWithTag ("HealthText").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			dead = true;
		}
		if (transform.position.y <= .75) {
			dead = true;
		}
		text.text = "Health: " + health;
	}

	public void TakeDamage(int n) {
		health -= n;
	}

	public bool isDead() {
		return dead;
	}
}
