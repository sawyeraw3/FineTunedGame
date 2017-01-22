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

				DealDamage dealDamage = newBall.GetComponent<DealDamage> ();
				dealDamage.damageDealt = maxDamageDealt;
				dealDamage.deathDelay = emitLifeSpan;

				newBall.GetComponent<WaveMovement> ().frequency = curFrequency;

				setColor (newBall, curFrequency);

			}
			timer = 0;
		}
	}

	void setColor(GameObject go, float freq) {
		Renderer rend = go.GetComponent<Renderer> ();
		int c = (int) freq;
		switch (c)
		{
		case 10:
			rend.material.SetColor ("_Color", Color.black);
			freqText.color = Color.black;
			break;
		case 15:
			rend.material.SetColor ("_Color", Color.grey);
			freqText.color = Color.grey;
			break;
		case 20:
			rend.material.SetColor ("_Color", Color.blue);
			freqText.color = Color.blue;
			break;
		case 25:
			rend.material.SetColor ("_Color", Color.green);
			freqText.color = Color.green;
			break;
		case 30:
			rend.material.SetColor ("_Color", Color.yellow);
			freqText.color = Color.yellow;
			break;
		case 35:
			rend.material.SetColor ("_Color", Color.red);
			freqText.color = Color.red;
			break;
		}
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
