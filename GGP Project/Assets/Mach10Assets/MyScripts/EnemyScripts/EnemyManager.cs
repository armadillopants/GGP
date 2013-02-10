using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {
	public List<GameObject> enemies = new List<GameObject>();

	// Use this for initialization
	void Start () {
		for(int i=0; i<enemies.Count; i++){
			Instantiate(enemies[i], 
						new Vector3(Random.Range(-10, 10),enemies[i].transform.position.y, enemies[i].transform.position.z), 
						enemies[i].transform.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void AddEnemy(GameObject enemy){
		for(int i=0; i<enemies.Count; i++){
			enemies.Add(enemy);
		}
	}
	
	void ClearEnemies(){
		enemies.Clear();
	}
}
