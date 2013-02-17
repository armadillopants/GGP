using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	private float startHealth = 0.0f;
	public float curHealth = 0.0f;

	// Use this for initialization
	void Start(){
		
	}
	
	// Update is called once per frame
	void Update(){
		
	}
	
	public void ModifyHealth(float amount){
		startHealth = amount;
		curHealth = startHealth;
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
}
