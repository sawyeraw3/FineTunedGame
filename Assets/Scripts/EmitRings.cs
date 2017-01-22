using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmitRings : MonoBehaviour {

	public GameObject ringObjectPart;
	public float curFrequency = 10f;
	public float maxFrequency = 35f;
	public int numParts;
	public int numColorsInLevel;
	public float emitDelay;
	public float emitLifeSpan;
	public float maxDamageDealt;
	public float waveSpeed = 5;

	public AudioClip noise;
	GameObject playerBody;
	GameObject ballSpawn;
	float timer;
	Vector3 center;
	Text freqText;
	GameManager gm;


	void Start()
	{
		gm = GameObject.Find ("LevelManager").GetComponent<GameManager>();
		timer = emitDelay;
		freqText = GameObject.FindGameObjectWithTag ("HUD").GetComponentInChildren<Text> ();
		playerBody = GameObject.Find ("Joints");
		freqText.text = "Hz: " + curFrequency.ToString();
		ballSpawn = gameObject.transform.FindChild ("BallSpawn").gameObject;
		center = ballSpawn.transform.position;
		setColor (playerBody, curFrequency);

	}

	void Update()
	{
		if (Input.GetButtonDown("Fire2") && curFrequency < 35) {
			curFrequency += 5;
			freqText.text = "Hz: " + curFrequency.ToString();
			setColor (playerBody, curFrequency);
		} else if (Input.GetButtonDown("Fire3") && curFrequency > 10) {
			curFrequency -= 5;
			freqText.text = "Hz: " + curFrequency.ToString();
			setColor (playerBody, curFrequency);
		}

		timer += Time.deltaTime;

		if (Input.GetButtonDown("Fire1") && timer >= emitDelay){
			center = ballSpawn.transform.position;

			for (int i = 0; i < numParts; i++)
			{
				int a = i * (360 / numParts);
				Vector3 pos = RandomCircle(center, .5f ,a);
				GameObject newBall;
				Vector3 dir = (pos - center);
				Quaternion travelDir = Quaternion.LookRotation (dir);

				newBall = Instantiate(ringObjectPart, pos, travelDir) as GameObject;
				newBall.GetComponent<WaveMovement> ().moveSpeed = waveSpeed;
				newBall.GetComponent<WaveMovement> ().frequency = emitLifeSpan * 10 * waveSpeed;

				DealDamage dealDamage = newBall.GetComponentInChildren<DealDamage> ();
				dealDamage.damageDealt = maxDamageDealt;
				dealDamage.deathDelay = emitLifeSpan;

				WaveMovement wm = newBall.GetComponentInChildren<WaveMovement> ();
				wm.frequency = curFrequency;
				Destroy (newBall, emitLifeSpan);

				Light ballLight = newBall.GetComponent<Light> ();

				ballLight.color = setColor (newBall, curFrequency);


			}
			GameObject noiseMaker = Instantiate (new GameObject (), center, Quaternion.identity);
			AudioSource n = noiseMaker.AddComponent<AudioSource> ();
			n.transform.position = center;
			n.clip = noise;
			n.pitch = 1 + (1.5f * ((curFrequency / 100) - 0.15f));
			n.Play ();
			StartCoroutine (FadeNoise (n));
			timer = 0;
		}
	}

	IEnumerator FadeNoise(AudioSource n) {
		float i = 0.0f;
		float step = 1.1f / emitLifeSpan;
		while (i <= 1.0f) {
			i += step * Time.deltaTime;
			n.volume = Mathf.Lerp (1, 0, i);
			yield return new WaitForFixedUpdate ();
		}
		Destroy (n);
	}

	Color setColor(GameObject go, float freq) {
		Renderer rend = go.GetComponentInChildren<Renderer> ();
		int c = (int) freq;
		switch (c)
		{
		case 10:
			rend.material.SetColor ("_Color", gm.Blue);
			freqText.color = gm.Blue;
			return gm.Blue;
		case 15:
			rend.material.SetColor ("_Color", gm.Cyan);
			freqText.color = gm.Cyan;
			return gm.Cyan;
		case 20:
			rend.material.SetColor ("_Color", gm.Green);
			freqText.color = gm.Green;
			return gm.Green;
		case 25:
			rend.material.SetColor ("_Color", gm.Orange);
			freqText.color = gm.Orange;
			return gm.Orange;
		case 30:
			rend.material.SetColor ("_Color", gm.Red);
			freqText.color = gm.Red;
			return gm.Red;
		case 35:
			rend.material.SetColor ("_Color", gm.Pink);
			freqText.color = gm.Pink;
			return gm.Pink;
		}
		return Color.black;
	}

	Vector3 RandomCircle(Vector3 center, float radius,int a)
	{
		float ang = a;
		Vector3 pos;
		//Switched x and z for spider
		pos.z = center.z + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
		pos.x = center.x + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		pos.y = center.y;
		return pos;
	}
}
