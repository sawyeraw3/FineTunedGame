using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public float curHealth;
	Slider slider;
	bool dead;
	public AudioClip hurt;
	public AudioClip die;
	AudioSource sound;

	// Use this for initialization
	void Start () {
		sound = GetComponentInChildren<AudioSource> ();
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
			StartCoroutine ("SoundAndDie");
		}
	}

	IEnumerator SoundAndDie() {
		sound.clip = die;
		sound.volume = .75f;
		sound = Instantiate(sound, sound.transform.position, sound.transform.rotation);
		Destroy (gameObject);
		sound.Play();
		yield return new WaitForSeconds (sound.clip.length);
		Destroy (sound);
	}

	public void TakeDamage(float damage) {
		if (!dead) {
			sound.clip = hurt;
			sound.volume = .75f;
			sound.Play ();
			curHealth -= damage;
		}
		if (curHealth <= 0) {
			dead = true;
			KillManager.kills += 1;
		}
	}
}
