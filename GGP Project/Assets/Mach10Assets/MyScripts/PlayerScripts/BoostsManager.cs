using UnityEngine;
using System.Collections;

public class BoostsManager : MonoBehaviour {
	
	public void SortBoosts(){
		if(gameObject.tag == "HealthBoost"){
			Health health = GameObject.Find("Player").GetComponent<Health>();
			health.AddHealth(50f);
		}
		if(gameObject.tag == "LifeBoost"){
			Lives lives = GameObject.Find("Player").GetComponent<Lives>();
			lives.AddLives(1f);
		}
		if(gameObject.tag == "ShieldBoost"){
			Health shieldHealth = GameObject.FindGameObjectWithTag("Shield").GetComponent<Health>();
			shieldHealth.AddHealth(10f);
		}
	}
}
