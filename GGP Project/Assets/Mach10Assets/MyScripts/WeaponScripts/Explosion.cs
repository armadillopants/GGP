using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	public float explosionRadius = 5.0f;
	public float explosionDamage = 10.0f;
	public float explosionTimeout = 1.0f;
	private Health health;

	// Use this for initialization
	void Start(){	
		Vector3 explosionPos = transform.position;
		
		// Apply damage to close by objects first
		Collider[] hits = Physics.OverlapSphere(explosionPos, explosionRadius);
		foreach(Collider hit in hits){
			// Calculate distance from the explosion position to the closest point on the collider
			Vector3 closestPoint = hit.ClosestPointOnBounds(explosionPos);
			float distance = Vector3.Distance(closestPoint, explosionPos);
			
			// The hit points we apply decrease with distance from the explosion point
			double damage = 1.0 - Mathf.Clamp01(distance/explosionRadius);
			damage *= explosionDamage;
			
			// Apply damage
			if(hit.tag == "Enemy" || hit.tag == "GroundEnemy"){
				health = hit.transform.parent.GetComponent<Health>();
				health.TakeDamage((float)damage);
			}
		}
		// Stop emitting particles
		if(particleEmitter){
			particleEmitter.emit = true;
			StartCoroutine("TurnOffParticles");
		}
		
		// Destroy the explosion after awhile
		Destroy(gameObject, explosionTimeout);
	}
	
	IEnumerator TurnOffParticles(){
		yield return new WaitForSeconds(0.5f);
		particleEmitter.emit = false;
	}
	
	public void ModifyExplosionRadius(float amount){
		explosionRadius = amount;
	}
	
	public void ModifyExplosionDamage(float amount){
		explosionDamage = amount;
	}
}