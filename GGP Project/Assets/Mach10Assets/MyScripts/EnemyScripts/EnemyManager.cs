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

	// Use this for initialization
	void Start(){
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
		secondsPassed += 1.5f*Time.deltaTime;
	}
	
	void SpawnEnemies(){
		if(secondsPassed > 5f){
			if(enemiesSpawned <= enemiesSpawnedPerLevel && maxEnemiesOnScreen <= 6){
				for(int i=0; i<enemies.Count; i++){
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
	
	void AddEnemy(GameObject enemy){
		for(int i=0; i<enemies.Count; i++){
			enemies.Add(enemy);
		}
	}
	
	void RemoveEnemy(GameObject enemy){
		for(int i=0; i<enemies.Count; i++){
			if(enemies[i] == enemy){
				enemies.RemoveAt(i);
				break;
			}
		}
	}
	
	void ClearEnemies(){
		enemies.Clear();
	}
}
