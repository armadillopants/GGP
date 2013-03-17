using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {
	public List<GameObject> airEnemies = new List<GameObject>();
	public List<GameObject> groundEnemies = new List<GameObject>();
	public GameObject[] enemiesOnScreen;
	public int enemiesSpawnedPerLevel;
	public int enemiesSpawned;
	public int maxEnemiesOnScreen = 0;
	private int totalRanks;
	private int currentRank = 0;
	public float secondsPassed = 0f;
	private LevelWin levelWin;
	private GameObject[] targets;
	Camera cam;
	float distance;
	float top;

	// Use this for initialization
	void Start(){
		enemiesOnScreen = GameObject.FindGameObjectsWithTag("Manager");
		cam = Camera.main;
		levelWin = GameObject.Find("LevelWin").GetComponent<LevelWin>();
		totalRanks = airEnemies.Count;
		enemiesSpawnedPerLevel = 100;//Random.Range(20, 60);
		targets = GameObject.FindGameObjectsWithTag("Target");
		distance = Vector3.Dot(cam.transform.forward, transform.position-cam.transform.position);
		top = cam.ViewportToWorldPoint(new Vector3(0, 1f, distance)).z;
	}
	
	// Update is called once per frame
	void Update(){
		if(enemiesOnScreen != null){
			enemiesOnScreen = GameObject.FindGameObjectsWithTag("Manager");
		}
		if(currentRank == totalRanks){
			SpawnAirEnemies();
			//SpawnGroundEnemies();
		} else {
			SpawnRanks();
		}
		maxEnemiesOnScreen = Mathf.Max(0, maxEnemiesOnScreen);
		if(enemiesSpawned >= enemiesSpawnedPerLevel && enemiesOnScreen.Length<=0){//maxEnemiesOnScreen <= 0){
			levelWin.LevelWon();
			ClearEnemies();
		}
		secondsPassed += 1.5f*Time.deltaTime;
	}
	
	void SpawnAirEnemies(){
		if(secondsPassed > 5f){
			for(int i=0; i<airEnemies.Count; i++){
				if(enemiesSpawned <= enemiesSpawnedPerLevel && enemiesOnScreen.Length <= 6){//maxEnemiesOnScreen <= 6){
					Instantiate(airEnemies[Random.Range(0, airEnemies.Count)], 
								new Vector3(Random.Range(-12, 12), airEnemies[i].transform.position.y, airEnemies[i].transform.position.z), 
								airEnemies[i].transform.rotation);
					enemiesSpawned++;
					//maxEnemiesOnScreen++;
				}
			}
			for(int i=0; i<groundEnemies.Count; i++){
				if(enemiesSpawned <= enemiesSpawnedPerLevel && enemiesOnScreen.Length<=6){//maxEnemiesOnScreen <= 6){
					foreach(GameObject target in targets){
						if(target.transform.position.z >= top){
							Instantiate(groundEnemies[Random.Range(0, groundEnemies.Count)], 
										new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z), 
										groundEnemies[i].transform.rotation);
							enemiesSpawned++;
							//maxEnemiesOnScreen++;
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
			//maxEnemiesOnScreen++;
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
