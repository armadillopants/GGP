using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	private float startHealth = 100.0f;
	public float curHealth = 0.0f;

	// Use this for initialization
	void Start(){
		curHealth = startHealth;
	}
	
	// Update is called once per frame
	void Update(){
		
	}
	
	public void ModifyHealth(float amount){
		curHealth = amount;
	}
	
	private void TakePlayerDamage(float damage){
		curHealth -= damage;
		if(curHealth <= 0){
			Die();
		}
	}
	
	void Die(){
		Destroy(gameObject);
	}
	
	void OnGUI(){
		GUILayout.Label("Health: " + curHealth);
	}
}
