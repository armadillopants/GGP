using UnityEngine;

public class CollisionHandler : MonoBehaviour {
	private Health health;
	private GameObject player;
	private GameObject shield;
	
	void Start(){
		health = transform.parent.GetComponent<Health>();
		player = GameObject.Find("Player");
		shield = GameObject.FindGameObjectWithTag("Shield");
	}
	
	void OnTriggerEnter(Collider hit){
		if(hit.tag == "Player"){
			Health shieldHealth = shield.GetComponent<Health>();
			if(shieldHealth.getHealth() > 0){
				shieldHealth.TakeDamage(10f);
				shield.renderer.enabled = true;
			} else {
				shield.renderer.enabled = false;
				Health playerHealth = player.GetComponentInChildren<Health>();
				playerHealth.TakeDamage(20f);
			}
			health.Die();
		}
	}
}
