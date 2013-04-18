using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {
	public List<GameObject> tutorialAirEnemies = new List<GameObject>();
	public List<GameObject> tutorialGroundEnemies = new List<GameObject>();
	public List<GameObject> airEnemies = new List<GameObject>();
	public List<GameObject> groundEnemies = new List<GameObject>();
	public List<GameObject> swarmers = new List<GameObject>();
	public GameObject boss;
	private float enemiesAllowed;
	public GameObject[] maxEnemiesOnScreen;
	public float enemiesSpawnedPerLevel;
	public float enemiesSpawned;
	private int totalRanks;
	private int currentRank = 0;
	private float[] secondsPassed = new float[4];
	private LevelWin levelWin;
	private GameObject[] targets;
	public bool spawnTutAirEnemies = false;
	public bool spawnTutGroundEnemies = false;
	bool once = true;
	public bool canSpawnEnemies = true;
	
	Camera cam;
	float distance;
	float top;
	float left;
	float right;

	// Use this for initialization
	void Start(){
		cam = Camera.main;
		levelWin = GameObject.Find("LevelWin").GetComponent<LevelWin>();
		totalRanks = airEnemies.Count;
		if(levelWin.curLevel == "Tutorial"){
			enemiesSpawnedPerLevel = 10f;
			enemiesAllowed = 4f;
		} else if(levelWin.curLevel == "Level1"){
			enemiesSpawnedPerLevel = 100f;
			enemiesAllowed = 6f;
		} else if(levelWin.curLevel == "Level2"){
			enemiesSpawnedPerLevel = 1f;
			enemiesAllowed = 8f;
		} else if(levelWin.curLevel == "Level3"){
			enemiesSpawnedPerLevel = 200f;
			enemiesAllowed = 10f;
		} else if(levelWin.curLevel == "Survival"){
			enemiesAllowed = 12f;
			enemiesSpawnedPerLevel = Mathf.Infinity;
		}
		targets = GameObject.FindGameObjectsWithTag("Target");
		distance = Vector3.Dot(cam.transform.forward, transform.position-cam.transform.position);
		top = cam.ViewportToWorldPoint(new Vector3(0, 1f, distance)).z;
		left = cam.ViewportToWorldPoint(new Vector3(0.3f, 0, distance)).x;
		right = cam.ViewportToWorldPoint(new Vector3(0.7f, 0, distance)).x;
	}
	
	// Update is called once per frame
	void Update(){
		if(maxEnemiesOnScreen != null){
			maxEnemiesOnScreen = GameObject.FindGameObjectsWithTag("Manager");
		}
		if(levelWin.curLevel != "Tutorial"){
			if(currentRank == totalRanks){
				if(canSpawnEnemies){
					SpawnAirEnemies();
					SpawnGroundEnemies();
					SpawnSwarmers();
				}
			} else {
				if(canSpawnEnemies){
					SpawnRanks();
				}
			}
		} else {
			if(spawnTutAirEnemies){
				SpawnTutorialAirEnemies();
			}
			if(spawnTutGroundEnemies){
				SpawnTutorialGroundEnemies();
			}
		}
		if(levelWin.curLevel == "Survival"){
			if(secondsPassed[3] > 120f){
				if(once){
					SpawnBoss();
					once = false;
					canSpawnEnemies = false;
				}
			}
			if(!GameObject.Find("Bee(Clone)")){
				canSpawnEnemies = true;
				once = true;
			}
		}
		if(enemiesSpawned >= enemiesSpawnedPerLevel && maxEnemiesOnScreen.Length<=0 && levelWin.curLevel != "Tutorial"){
			if(once){
				SpawnBoss();
				canSpawnEnemies = false;
				DestroyEnemies();
				once = false;
			}
			if(!GameObject.Find("Bee(Clone)")){
				levelWin.LevelWon();
			}
		}
		
		switch(levelWin.curLevel){
		case "Tutorial":
			secondsPassed[0] += 1f*Time.deltaTime;
			break;
		case "Level1":
			secondsPassed[0] += 0.8f*Time.deltaTime;
			secondsPassed[1] += 0.6f*Time.deltaTime;
			secondsPassed[2] += 0.3f*Time.deltaTime;
			break;
		case "Level2":
			secondsPassed[0] += 1.3f*Time.deltaTime;
			secondsPassed[1] += 1f*Time.deltaTime;
			secondsPassed[2] += 0.8f*Time.deltaTime;
			break;
		case "Level3":
			secondsPassed[0] += 1.5f*Time.deltaTime;
			secondsPassed[1] += 1.2f*Time.deltaTime;
			secondsPassed[2] += 1f*Time.deltaTime;
			break;
		case "Survival":
			secondsPassed[0] += 2f*Time.deltaTime;
			secondsPassed[1] += 1.5f*Time.deltaTime;
			secondsPassed[2] += 1.3f*Time.deltaTime;
			if(canSpawnEnemies){
				secondsPassed[3] += 1f*Time.deltaTime;
			}
			break;
		}
	}
	
	void SpawnAirEnemies(){
		if(secondsPassed[0] > 5f){
			for(int i=0; i<airEnemies.Count; i++){
				if(enemiesSpawned <= enemiesSpawnedPerLevel && maxEnemiesOnScreen.Length <= enemiesAllowed){
					Instantiate(airEnemies[Random.Range(0, airEnemies.Count)], 
						new Vector3(Random.Range(left, right), airEnemies[i].transform.position.y, airEnemies[i].transform.position.z), 
						Quaternion.identity);
					enemiesSpawned++;
				}
			}
			secondsPassed[0] = 0f;
		}
	}
	
	void SpawnGroundEnemies(){
		if(secondsPassed[1] > 6f){
			for(int i=0; i<groundEnemies.Count; i++){
				if(enemiesSpawned <= enemiesSpawnedPerLevel && maxEnemiesOnScreen.Length <= enemiesAllowed){
					foreach(GameObject target in targets){
						if(target.transform.position.z >= top){
							Instantiate(groundEnemies[Random.Range(0, groundEnemies.Count)], 
								target.transform.position, 
								Quaternion.identity);
							enemiesSpawned++;
						}
					}
				}
			}
			secondsPassed[1] = 0f;
		}
	}
	
	void SpawnSwarmers(){
		if(secondsPassed[2] > 7f){
			for(int i=0; i<swarmers.Count; i++){
				if(enemiesSpawned <= enemiesSpawnedPerLevel && maxEnemiesOnScreen.Length <= enemiesAllowed){
					Instantiate(swarmers[i], 
						new Vector3(Random.Range(transform.position.x-4, transform.position.x+4), swarmers[i].transform.position.y, Random.Range(25, 35)),
						Quaternion.identity);
					enemiesSpawned++;
				}
			}
			secondsPassed[2] = 0f;
		}
	}
	
	void SpawnRanks(){
		if(secondsPassed[0] > 3f){
			Instantiate(airEnemies[currentRank],
				new Vector3(Random.Range(left, right), airEnemies[currentRank].transform.position.y, airEnemies[currentRank].transform.position.z),
				Quaternion.identity);
			secondsPassed[0] = 0f;
			currentRank++;
		}
	}

	void SpawnTutorialAirEnemies(){
		if(secondsPassed[0] > 3f){
			for(int i=0; i<tutorialAirEnemies.Count; i++){
				if(enemiesSpawned <= enemiesSpawnedPerLevel && maxEnemiesOnScreen.Length <= enemiesAllowed){
					Instantiate(tutorialAirEnemies[Random.Range(0, tutorialAirEnemies.Count)],
						new Vector3(Random.Range(left, right), tutorialAirEnemies[i].transform.position.y, tutorialAirEnemies[i].transform.position.z),
						Quaternion.identity);
					enemiesSpawned++;
				}
			}
			secondsPassed[0] = 0f;
		}
	}

	void SpawnTutorialGroundEnemies(){
		if(secondsPassed[0] > 3f){
			for(int i=0; i<tutorialGroundEnemies.Count; i++){
				if(enemiesSpawned <= enemiesSpawnedPerLevel && maxEnemiesOnScreen.Length <= enemiesAllowed){
					foreach(GameObject target in targets){
						if(target.transform.position.z >= top){
							Instantiate(tutorialGroundEnemies[Random.Range(0, tutorialGroundEnemies.Count)], 
								target.transform.position, 
								Quaternion.identity);
							enemiesSpawned++;
						}
					}
				}
			}
			secondsPassed[0] = 0f;
		}
	}
	
	void SpawnBoss(){
		Instantiate(boss, boss.transform.position, Quaternion.identity);
		secondsPassed[3] = 0f;
	}
	
	public void DestroyEnemies(){
		GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Manager");
		foreach(GameObject enemy in enemyList){
			Destroy(enemy);
		}
		GameObject[] bulletList = GameObject.FindGameObjectsWithTag("Bullet");
		foreach(GameObject bullet in bulletList){
			Destroy(bullet);
		}
	}
}
