using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public float curHealth;
	Slider slider;
	bool dead;

	// Use this for initialization
	void Start () {
		slider = gameObject.transform.FindChild("DamageCanvas").GetComponentInChildren<Slider> ();
		slider.interactable = false;
		slider.maxValue = curHealth;
		slider.value = curHealth;
		dead = false;
	}
	
	// Update is called once per frame
	void Update () {
		slider.value = curHealth;
		if (dead) {
			Destroy (gameObject);
			KillManager.kills += 1;
		}
	}

	public void TakeDamage(float damage) {
		if (!dead) {
			curHealth -= damage;
		}
		if (curHealth <= 0) {
			dead = true;
		}
	}
}
