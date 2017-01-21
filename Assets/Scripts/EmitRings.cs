using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmitRings : MonoBehaviour {

	public GameObject ringObjectPart;
	public float curFrequency = 10f;
	public int numParts;
	public float emitSpeed;
	public float emitFrequency;

	public float maxDamageDealt;

	float timer;
	Vector3 center;
	Text freqText;


	// Use this for initialization
	void Start()
	{
		timer = emitFrequency;
		freqText = GameObject.FindGameObjectWithTag ("HUD").GetComponentInChildren<Text> ();
		freqText.text = "Hz: " + curFrequency.ToString();
	}

	void Update()
	{
		if (Input.GetButtonDown("Fire2") && curFrequency < 35) {
			curFrequency += 5;
			freqText.text = "Hz: " + curFrequency.ToString();
		} else if (Input.GetButtonDown("Fire3") && curFrequency > 10) {
			curFrequency -= 5;
			freqText.text = "Hz: " + curFrequency.ToString();
		}

		timer += Time.deltaTime;

		if (Input.GetButtonDown("Fire1")){
			int offset = 0;
			center = transform.position;
			for (int i = 0; i < numParts; i++)
			{
				int a = i * (360 / numParts);
				Vector3 pos = RandomCircle(center, .5f ,a);
				GameObject newBall;
				Vector3 dir = (pos - center);
				Quaternion travelDir = Quaternion.LookRotation (dir);

				newBall = Instantiate(ringObjectPart, pos, travelDir) as GameObject;

				newBall.GetComponent<DealDamage> ().damageDealt = maxDamageDealt;
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
			break;
		case 15:
			rend.material.SetColor ("_Color", Color.grey);
			break;
		case 20:
			rend.material.SetColor ("_Color", Color.blue);
			break;
		case 25:
			rend.material.SetColor ("_Color", Color.green);
			break;
		case 30:
			rend.material.SetColor ("_Color", Color.yellow);
			break;
		case 35:
			rend.material.SetColor ("_Color", Color.red);
			break;
		}
	}

	Vector3 RandomCircle(Vector3 center, float radius,int a)
	{
		float ang = a;
		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
		pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		pos.y = center.y;
		return pos;
	}
}
