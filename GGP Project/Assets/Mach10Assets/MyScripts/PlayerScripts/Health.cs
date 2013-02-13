using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
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
		startHealth = amount;
	}
	
	public void TakeDamage(float damage){
		curHealth -= damage;
		if(curHealth <= 0){
			Die();
		}
	}
	
	public void Die(){
		Destroy(gameObject);
		//Destroy(renderer.material);
	}
	
	void OnGUI(){
		GUILayout.Label("Health: " + curHealth);
	}
}
