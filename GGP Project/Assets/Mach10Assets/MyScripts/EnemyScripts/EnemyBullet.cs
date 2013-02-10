using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {
	public GameObject explosion;
	public float speed = 10f;
	public float range = 10f;
	public float lifeTime = 0.0f;
	private float collisionRadius = 0.5f;
	private float damage = 5f;
	private float distance;
	
	void Start(){
		Invoke("Kill", lifeTime);
	}
	
	// Update is called once per frame
	void Update(){
		Collider[] hits = Physics.OverlapSphere(collider.transform.position, collisionRadius);
		foreach(Collider c in hits){
			c.collider.gameObject.SendMessageUpwards("TakePlayerDamage", damage, SendMessageOptions.DontRequireReceiver);
		}
		transform.Translate(Vector3.forward * Time.deltaTime * speed);
		distance -= speed * Time.deltaTime;
	}
	
	void Kill(){
		if(explosion != null){
			Instantiate(explosion, transform.position, transform.rotation);
		}
		// Stop emitting particles in any children
		ParticleEmitter emitter = GetComponentInChildren<ParticleEmitter>();
		if (emitter){
			emitter.emit = false;
		}

		// Detach children - We do this to detach the trail rendererer which should be set up to auto destruct
		transform.DetachChildren();
		
		// Destroy the projectile
		Destroy(gameObject);
	}
	
	public void ModifyDamage(float amount){
		damage = amount;
	}
	
	public void ModifyLifeTime(float amount){
		lifeTime = amount;
	}
}