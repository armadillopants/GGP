using UnityEngine;

public class BoostsManager : MonoBehaviour {
	private GameObject curBoost;
	public AudioClip healthBoostSound;
	public AudioClip LifeBoostSound;
	public AudioClip ShieldBoostSound;
	
	public void SortBoosts(){
		curBoost = GameObject.FindGameObjectWithTag("Boost");
		if(curBoost.name == "HealthBoost"){
			AudioSource.PlayClipAtPoint(healthBoostSound, transform.position, 0.8f);
			Health health = GameObject.Find("Player").GetComponent<Health>();
			health.AddHealth(20f);
		}
		if(curBoost.name == "LifeBoost"){
			AudioSource.PlayClipAtPoint(LifeBoostSound, transform.position, 0.8f);
			Lives lives = GameObject.Find("Player").GetComponent<Lives>();
			lives.AddLives(1);
		}
		if(curBoost.name == "ShieldBoost"){
			AudioSource.PlayClipAtPoint(ShieldBoostSound, transform.position, 0.8f);
			Health shieldHealth = GameObject.FindGameObjectWithTag("Shield").GetComponent<Health>();
			shieldHealth.AddHealth(10f);
		}
	}
}
