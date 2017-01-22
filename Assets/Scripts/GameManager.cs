using UnityEngine;

using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject enemy;
	public Transform[] spawnPoints;
	public int maxEnemies;
	public int secBetweenSpawn = 5;
	public int waveEnemyIncrease;
	int totalEnemies = 0;
	int enemiesSpawned = 0;
	int whichSpawn = 0;
	float timer;

	// Use this for initialization
	void Start () {
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
		}
	}

	void SpawnEnemies()
	{
		whichSpawn = Random.Range (0, spawnPoints.Length);
		GameObject newEnemy = Instantiate (enemy, spawnPoints [whichSpawn].transform.position, spawnPoints [whichSpawn].transform.rotation) as GameObject;
		newEnemy.GetComponentInChildren<Renderer> ().material.color = Color.yellow;
		enemiesSpawned ++;
	}
}