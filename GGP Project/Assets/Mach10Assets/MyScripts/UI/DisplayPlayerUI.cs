using UnityEngine;
using System.Collections;

public class DisplayPlayerUI : MonoBehaviour {
	private Health health;
	private Health shieldHealth;
	private Lives lives;
	private GameObject player;
	private GameObject shield;
	private EnemyManager manager;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		health = player.GetComponent<Health>();
		health.ModifyHealth(100f);
		
		shield = GameObject.FindGameObjectWithTag("Shield");
		shieldHealth = shield.GetComponent<Health>();
		shieldHealth.ModifyHealth(50f);
		
		lives = player.GetComponent<Lives>();
		lives.ModifyLives(3f);
		manager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
	}
	
	void Update(){
		if(lives.getLives() <= 0){
			manager.ClearEnemies();
		}
	}
	
	void OnGUI(){
		if(player){
			GUILayout.Label("Health: " + health.getHealth());
			GUILayout.Label("Lives: " + lives.getLives());
			GUILayout.Label("Shield: " + shieldHealth.getHealth());
		}
	}
}
