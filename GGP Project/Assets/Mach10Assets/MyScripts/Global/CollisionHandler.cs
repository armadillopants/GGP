using UnityEngine;
using System.Collections;

public class CollisionHandler : MonoBehaviour {
	private Health health;
	private GameObject player;
	
	void Start(){
		health = transform.parent.GetComponent<Health>();
		player = GameObject.Find("Player");
	}
	
	void OnTriggerEnter(Collider hit){
		if(hit.tag == "Player"){
			Health playerHealth = player.GetComponentInChildren<Health>();
			playerHealth.TakeDamage(20f);
			health.Die();
		}
	}
}
