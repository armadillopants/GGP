using UnityEngine;

public class BoostsManager : MonoBehaviour {
	private GameObject curBoost;
	
	public void SortBoosts(){
		curBoost = GameObject.FindGameObjectWithTag("Boost");
		if(curBoost.name == "HealthBoost"){
			Health health = GameObject.Find("Player").GetComponent<Health>();
			health.AddHealth(20f);
		}
		if(curBoost.name == "LifeBoost"){
			Lives lives = GameObject.Find("Player").GetComponent<Lives>();
			lives.AddLives(1);
		}
		if(curBoost.name == "ShieldBoost"){
			Health shieldHealth = GameObject.FindGameObjectWithTag("Shield").GetComponent<Health>();
			shieldHealth.AddHealth(10f);
		}
	}
}
