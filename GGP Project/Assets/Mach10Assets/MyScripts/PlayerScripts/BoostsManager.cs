using UnityEngine;

public class BoostsManager : MonoBehaviour {
	private GameObject curBoost;
	
	public void SortBoosts(){
		curBoost = GameObject.FindGameObjectWithTag("Boost");
		if(curBoost.name == "HealthBoost(Clone)"){
			Health health = GameObject.Find("Player").GetComponent<Health>();
			health.AddHealth(50f);
		}
		if(curBoost.name == "LifeBoost(Clone)"){
			Lives lives = GameObject.Find("Player").GetComponent<Lives>();
			lives.AddLives(1f);
		}
		if(curBoost.name == "ShieldBoost(Clone)"){
			Health shieldHealth = GameObject.FindGameObjectWithTag("Shield").GetComponent<Health>();
			shieldHealth.AddHealth(10f);
		}
	}
}
