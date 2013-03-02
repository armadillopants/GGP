using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {
	public List<GameObject> enemies = new List<GameObject>();
	public int enemiesSpawnedPerLevel;
	public int enemiesSpawned;
	public int maxEnemiesOnScreen = 0;
	private int totalRanks;
	private int currentRank = 0;
	private float secondsPassed = 0f;
	private LevelWin levelWin;

	// Use this for initialization
	void Start(){
		levelWin = GameObject.Find("LevelWin").GetComponent<LevelWin>();
		totalRanks = enemies.Count;
		enemiesSpawnedPerLevel = Random.Range(20, 60);
	}
	
	// Update is called once per frame
	void Update(){
		if(currentRank == totalRanks){
			SpawnEnemies();
		} else {
			SpawnRanks();
		}
		maxEnemiesOnScreen = Mathf.Max(0, maxEnemiesOnScreen);
		if(enemiesSpawned >= enemiesSpawnedPerLevel && maxEnemiesOnScreen <= 0){
			levelWin.LevelWon();
		}
		secondsPassed += 1.5f*Time.deltaTime;
	}
	
	void SpawnEnemies(){
		if(secondsPassed > 5f){
			for(int i=0; i<enemies.Count; i++){
				if(enemiesSpawned <= enemiesSpawnedPerLevel && maxEnemiesOnScreen <= 6){
					Instantiate(enemies[Random.Range(0, enemies.Count)], 
								new Vector3(Random.Range(-10, 10), enemies[i].transform.position.y, enemies[i].transform.position.z), 
								enemies[i].transform.rotation);
					enemiesSpawned++;
					maxEnemiesOnScreen++;
					secondsPassed = 0f;
				}
			}
		}
	}
	
	void SpawnRanks(){
		if(secondsPassed > 3f){
			Instantiate(enemies[currentRank],
						new Vector3(Random.Range(-10, 10), enemies[currentRank].transform.position.y, enemies[currentRank].transform.position.z),
						enemies[currentRank].transform.rotation);
			secondsPassed = 0f;
			currentRank++;
			maxEnemiesOnScreen++;
		}
	}
	
	public void ModifyEnemiesPerLevel(int amount){
		enemiesSpawnedPerLevel = amount;
	}
	
	public void AddEnemy(GameObject enemy){
		for(int i=0; i<enemies.Count; i++){
			enemies.Add(enemy);
		}
	}
	
	public void RemoveEnemy(GameObject enemy){
		for(int i=0; i<enemies.Count; i++){
			if(enemies[i] == enemy){
				enemies.RemoveAt(i);
				break;
			}
		}
	}
	
	public void ClearEnemies(){
		GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Manager");
		foreach(GameObject enemy in enemyList){
			Destroy(enemy);
		}
		enemies.Clear();
	}
}
