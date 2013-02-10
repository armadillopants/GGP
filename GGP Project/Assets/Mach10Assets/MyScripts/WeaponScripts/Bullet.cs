using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	private Rigidbody rigid;
	public GameObject explosion;
	public float bulletSpeed = 50f;
	public float bulletDistance = 100f;
	private float lifeTime = 0.0f;
	//private float spawnTime = 0.0f;
	private float damage = 0.0f;
	private float collisionRadius = 1.0f;

	// Use this for initialization
	void Start(){
		rigid = rigidbody;
		//spawnTime = Time.time;
		Invoke("Kill", lifeTime);
	}
	
	// Update is called once per frame
	void Update(){
		Collider[] hits = Physics.OverlapSphere(collider.transform.position, collisionRadius);
		foreach(Collider c in hits){
			c.collider.gameObject.SendMessageUpwards("TakeEnemyDamage", damage, SendMessageOptions.DontRequireReceiver);
		}
		rigid.velocity += transform.TransformDirection(Vector3.forward * bulletSpeed);
		bulletDistance -= bulletSpeed * Time.deltaTime;
	}
	
	void OnTriggerEnter(Collider hit){
		if(hit.tag == "Enemy"){
			// Call to destroy projectile
			Kill();
		}
		if(hit.tag == "Ground"){
			Kill();
		}
	}	
	
	void OnTriggerStay(Collider hit){
		if(hit.tag == "Enemy"){
			// Call to destroy projectile
			Kill();
		}
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
