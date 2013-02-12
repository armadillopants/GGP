using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {
	public GameObject explosion;
	private float speed = 0f;
	private float lifeTime = 0.0f;
	private float collisionRadius = 0.5f;
	private float damage = 0f;
	
	void Start(){
		Invoke("Kill", lifeTime);
	}
	
	// Update is called once per frame
	void Update(){
		Collider[] hits = Physics.OverlapSphere(collider.transform.position, collisionRadius);
		foreach(Collider c in hits){
			c.collider.gameObject.SendMessageUpwards("TakePlayerDamage", damage, SendMessageOptions.DontRequireReceiver);
		}
		transform.Translate(Vector3.forward*speed*Time.deltaTime);
	}
	
	void OnTriggerEnter(Collider hit){
		if(hit.tag == "Player"){
			Kill();
		}
	}
	
	void Kill(){
		if(explosion != null){
			Instantiate(explosion, transform.position, transform.rotation);
		}
		// Stop emitting particles in any children
		ParticleEmitter emitter = GetComponentInChildren<ParticleEmitter>();
		if(emitter){
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
	
	public void ModifySpeed(float amount){
		speed = amount;
	}
	
	public void ModifyLifeTime(float amount){
		lifeTime = amount;
	}
}