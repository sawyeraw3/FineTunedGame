using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject[] enemyTypes;
	public Transform[] spawnPoints;
	public int maxEnemies;
	public int secBetweenSpawn = 5;
	public int waveEnemyIncrease;
	public float difficultySpread = 10;
	int totalEnemies = 0;
	int enemiesSpawned = 0;
	int whichSpawn = 0;
	int whichEnemy = 0;
	float difficulty = 1;

	float timer;

	public readonly Color Blue = new Color((56f/255f),(63f/255f),(188f/255f), 1);
	public readonly Color Cyan = new Color((1f/255f),1,1, 1);
	public readonly Color Green = new Color((103f/255f),1,(100f/255f), 1);
	public readonly Color Orange = new Color(1,(173f/255f),(53f/255f), 1);
	public readonly Color Red = new Color(1,(49f/255f),(58f/255f), 1);
	public readonly Color Pink = new Color(1,(36f/255f),(239f/255f), 1);

	Color[] cols;

	// Use this for initialization
	void Start () {
		cols = new Color[]{Blue, Cyan, Green, Orange, Red, Pink};
		totalEnemies += maxEnemies;
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (enemiesSpawned < maxEnemies && timer > secBetweenSpawn) {
			SpawnEnemies ();
			timer = 0;
		} else if (KillManager.kills == totalEnemies) {
			enemiesSpawned = 0;
			maxEnemies += waveEnemyIncrease;
			totalEnemies += maxEnemies;
			WaveManager.wave ++;
			difficulty++;
		}
	}

	void SpawnEnemies() {
		int e = 0;
		if (Random.Range (0f, difficultySpread) <= difficulty)
			e = 1;
		whichSpawn = Random.Range (0, spawnPoints.Length);
		GameObject newEnemy = Instantiate (enemyTypes[e], spawnPoints [whichSpawn].transform.position, Quaternion.identity) as GameObject;
		int i = Random.Range (0, 5);
		Color c = cols [i];
		Renderer rend = newEnemy.transform.FindChild ("Colored").GetComponent<Renderer>();
		rend.material.color = c;
		Light[] lights = newEnemy.GetComponentsInChildren<Light> ();
		foreach (Light l in lights) {
			l.color = c;
		}
		enemiesSpawned ++;
	}
}