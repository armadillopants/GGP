using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	private Transform trans;
	public GameObject explosion;
	private GameObject player;
	private GameObject enemy;
	private GameObject groundEnemy;
	private float bulletSpeed = 50f;
	private float lifeTime = 0.0f;
	private float damage = 0.0f;
	private float collisionRadius = 1.0f;
	public GameObject target;
	public bool isHoming = false;
	private float damp = 6.0f;
	private Health health;

	// Use this for initialization
	void Start(){
		trans = transform;
		Invoke("Kill", lifeTime);
		player = GameObject.Find("Player");
		enemy = GameObject.FindGameObjectWithTag("Enemy");
		groundEnemy = GameObject.FindGameObjectWithTag("GroundEnemy");
	}
	
	// Update is called once per frame
	void Update(){
		target = FindNearestEnemey();
		/*Collider[] hits = Physics.OverlapSphere(collider.transform.position, collisionRadius);
		foreach(Collider c in hits){
			c.collider.gameObject.SendMessageUpwards("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
		}*/
		if(isHoming){
			if(target){
				trans.Translate(Vector3.forward*bulletSpeed*Time.deltaTime);
				Quaternion rotate = Quaternion.LookRotation(target.transform.position - trans.position);
				trans.rotation = Quaternion.Slerp(trans.rotation, rotate, Time.deltaTime * damp);
			} else {
				trans.Translate(Vector3.forward*bulletSpeed*Time.deltaTime);
			}
		} else {
			trans.Translate(Vector3.forward*bulletSpeed*Time.deltaTime);
		}
	}
	
	void OnTriggerEnter(Collider hit){
		if(hit.tag == "Enemy"){
			health = hit.GetComponent<Health>();
			health.TakeDamage(damage);
			Kill();
		}
		if(hit.tag == "Player"){
			health = player.GetComponent<Health>();
			health.TakeDamage(damage);
			Kill();
		}
		if(hit.tag == "Ground"){
			Kill();
		}
		if(hit.tag == "GroundEnemy"){
			health = hit.GetComponent<Health>();
			health.TakeDamage(damage);
			Kill();
		}
	}
	
	void Kill(){
		if(explosion != null){
			Instantiate(explosion, trans.position, trans.rotation);
		}
		// Stop emitting particles in any children
		ParticleEmitter emitter = GetComponentInChildren<ParticleEmitter>();
		if(emitter){
			emitter.emit = false;
		}

		// Detach children - We do this to detach the trail rendererer which should be set up to auto destruct
		trans.DetachChildren();
		
		// Destroy the projectile
		Destroy(gameObject);
	}
	
	public void ModifyDamage(float amount){
		damage = amount;
	}
	
	public void ModifySpeed(float amount){
		bulletSpeed = amount;
	}
	
	public void ModifyLifeTime(float amount){
		lifeTime = amount;
	}
	
	GameObject FindNearestEnemey(){
		GameObject[] enemyList;
		enemyList = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		
		foreach(GameObject enemyCheck in enemyList){
			Vector3 diff = (enemyCheck.transform.position - trans.position);
			float curDist = diff.sqrMagnitude;
			if(curDist < distance){
				closest = enemyCheck;
				distance = curDist;
			}
		}
		return closest;
	}
}
