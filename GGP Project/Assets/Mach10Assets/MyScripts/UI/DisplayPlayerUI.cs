using UnityEngine;
using System.Collections;

public class DisplayPlayerUI : MonoBehaviour {
	private Health health;
	private Lives lives;

	// Use this for initialization
	void Start () {
		GameObject player = GameObject.Find("Player");
		health = player.GetComponent<Health>();
		health.ModifyHealth(100f);
		lives = player.GetComponent<Lives>();
		lives.ModifyLives(3);
	}
	
	void OnGUI(){
		GUILayout.Label("Health: " + health.curHealth);
		GUILayout.Label("Lives: " + lives.curLives);
	}
}
