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

	static Color Blue = new Color((56f/255f),(63f/255f),(188f/255f), 1);
	static Color Cyan = new Color((1f/255f),1,1, 1);
	static Color Pink = new Color(1,(36f/255f),(239f/255f), 1);
	static Color Orange = new Color(1,(97f/255f),(53f/255f), 1);
	static Color Red = new Color(1,(49f/255f),(58f/255f), 1);
	static Color Green = new Color((103f/255f),1,(100f/255f), 1);

	AudioSource noise;
	GameObject playerBody;
	GameObject ballSpawn;
	float timer;
	Vector3 center;
	Text freqText;


	// Use this for initialization
	void Start()
	{
		
		timer = emitDelay;
		freqText = GameObject.FindGameObjectWithTag ("HUD").GetComponentInChildren<Text> ();
		playerBody = GameObject.Find ("Joints");
		freqText.text = "Hz: " + curFrequency.ToString();
		ballSpawn = gameObject.transform.FindChild ("BallSpawn").gameObject;
		center = ballSpawn.transform.position;
		setColor (playerBody, curFrequency);
		noise = GetComponentInChildren<AudioSource> ();
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

				DealDamage dealDamage = newBall.GetComponentInChildren<DealDamage> ();
				dealDamage.damageDealt = maxDamageDealt;
				dealDamage.deathDelay = emitLifeSpan;

				WaveMovement wm = newBall.GetComponentInChildren<WaveMovement> ();
				wm.frequency = curFrequency;
				Destroy (newBall, emitLifeSpan);

				Light ballLight = newBall.GetComponent<Light> ();

				ballLight.color = setColor (newBall, curFrequency);


			}
			GameObject noiseMaker = new GameObject ();
			noiseMaker.transform.position = center;

			AudioSource n = noiseMaker.AddComponent<AudioSource> ();
			n.clip = noise.clip;
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
			rend.material.SetColor ("_Color", Blue);
			freqText.color = Blue;
			return Blue;
			break;
		case 15:
			rend.material.SetColor ("_Color", Cyan);
			freqText.color = Cyan;
			return Cyan;
			break;
		case 20:
			rend.material.SetColor ("_Color", Green);
			freqText.color = Green;
			return Green;
			break;
		case 25:
			rend.material.SetColor ("_Color", Orange);
			freqText.color = Orange;
			return Orange;
			break;
		case 30:
			rend.material.SetColor ("_Color", Red);
			freqText.color = Red;
			return Red;
			break;
		case 35:
			rend.material.SetColor ("_Color", Pink);
			freqText.color = Pink;
			return Pink;
			break;
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
