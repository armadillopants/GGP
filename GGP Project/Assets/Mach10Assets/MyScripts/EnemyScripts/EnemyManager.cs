using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {
	public List<GameObject> airEnemies = new List<GameObject>();
	public List<GameObject> groundEnemies = new List<GameObject>();
	public GameObject[] maxEnemiesOnScreen;
	public int enemiesSpawnedPerLevel;
	public int enemiesSpawned;
	private int totalRanks;
	private int currentRank = 0;
	private float secondsPassed = 0f;
	private LevelWin levelWin;
	private GameObject[] targets;
	Camera cam;
	float distance;
	float top;

	// Use this for initialization
	void Start(){
		maxEnemiesOnScreen = GameObject.FindGameObjectsWithTag("Manager");
		cam = Camera.main;
		levelWin = GameObject.Find("LevelWin").GetComponent<LevelWin>();
		totalRanks = airEnemies.Count;
		enemiesSpawnedPerLevel = 100;
		targets = GameObject.FindGameObjectsWithTag("Target");
		distance = Vector3.Dot(cam.transform.forward, transform.position-cam.transform.position);
		top = cam.ViewportToWorldPoint(new Vector3(0, 1f, distance)).z;
	}
	
	// Update is called once per frame
	void Update(){
		if(maxEnemiesOnScreen != null){
			maxEnemiesOnScreen = GameObject.FindGameObjectsWithTag("Manager");
		}
		if(currentRank == totalRanks){
			SpawnAirEnemies();
			//SpawnGroundEnemies();
		} else {
			SpawnRanks();
		}
		if(enemiesSpawned >= enemiesSpawnedPerLevel && maxEnemiesOnScreen.Length<=0){
			levelWin.LevelWon();
			ClearEnemies();
		}
		secondsPassed += 0.7f*Time.deltaTime;
	}
	
	void SpawnAirEnemies(){
		if(secondsPassed > 5f){
			for(int i=0; i<airEnemies.Count; i++){
				if(enemiesSpawned <= enemiesSpawnedPerLevel && maxEnemiesOnScreen.Length <= 6){
					Instantiate(airEnemies[Random.Range(0, airEnemies.Count)], 
								new Vector3(Random.Range(-12, 12), airEnemies[i].transform.position.y, airEnemies[i].transform.position.z), 
								Quaternion.identity);
					enemiesSpawned++;
				}
			}
			for(int i=0; i<groundEnemies.Count; i++){
				if(enemiesSpawned <= enemiesSpawnedPerLevel && maxEnemiesOnScreen.Length<=6){
					foreach(GameObject target in targets){
						if(target.transform.position.z >= top){
							Instantiate(groundEnemies[Random.Range(0, groundEnemies.Count)], 
										new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z), 
										groundEnemies[i].transform.rotation);
							enemiesSpawned++;
						}
					}
				}
			}
			secondsPassed = 0f;
		}
	}
	
	/*void SpawnGroundEnemies(){
		if(secondsPassed > 5f){
			for(int i=0; i<groundEnemies.Count; i++){
				if(enemiesSpawned <= enemiesSpawnedPerLevel && maxEnemiesOnScreen <= 6){
					foreach(GameObject target in targets){
						if(target.transform.position.z >= top){
							Instantiate(groundEnemies[Random.Range(0, groundEnemies.Count)], 
										new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z), 
										groundEnemies[i].transform.rotation);
							enemiesSpawned++;
							maxEnemiesOnScreen++;
						}
					}
				}
			}
			secondsPassed = 0f;
		}
	}*/
	
	void SpawnRanks(){
		if(secondsPassed > 3f){
			Instantiate(airEnemies[currentRank],
						new Vector3(Random.Range(-12, 12), airEnemies[currentRank].transform.position.y, airEnemies[currentRank].transform.position.z),
						airEnemies[currentRank].transform.rotation);
			secondsPassed = 0f;
			currentRank++;
		}
	}
	
	public void ModifyEnemiesPerLevel(int amount){
		enemiesSpawnedPerLevel = amount;
	}
	
	public void AddEnemy(GameObject enemy){
		for(int i=0; i<airEnemies.Count; i++){
			airEnemies.Add(enemy);
		}
	}
	
	public void RemoveEnemy(GameObject enemy){
		for(int i=0; i<airEnemies.Count; i++){
			if(airEnemies[i] == enemy){
				airEnemies.RemoveAt(i);
				break;
			}
		}
	}
	
	public void ClearEnemies(){
		GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Manager");
		foreach(GameObject enemy in enemyList){
			Destroy(enemy);
		}
		airEnemies.Clear();
		groundEnemies.Clear();
	}
}
