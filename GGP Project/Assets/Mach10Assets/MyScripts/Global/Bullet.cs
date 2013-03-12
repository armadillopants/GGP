using UnityEngine;

public class Bullet : MonoBehaviour {
	private Transform trans;
	public GameObject explosion;
	public float bulletSpeed = 0.0f;
	public float lifeTime = 0.0f;
	public float damage = 0.0f;
	public GameObject target;
	public bool isHoming = false;
	private bool canShoot = false;
	private float damp = 6.0f;
	private Health health;
	Camera cam;

	// Use this for initialization
	void Start(){
		trans = transform;
		Invoke("Kill", lifeTime);
		cam = Camera.main;
		canShoot = false;
	}
	
	// Update is called once per frame
	void Update(){
		float distance = Vector3.Dot(cam.transform.forward, trans.position - cam.transform.position);
		float top = cam.ViewportToWorldPoint(new Vector3(0, 1f, distance)).z;
		float down = cam.ViewportToWorldPoint(new Vector3(0, 0f, distance)).z;
		if(trans.position.z >= top || trans.position.z <= down){
			Kill();
		}
		if(trans.position.z <= top && trans.position.z >= down){
			canShoot = true;
		} else {
			canShoot = false;
		}
		
		if(canShoot){
			if(isHoming){
				target = FindNearestEnemey();
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
	}
	
	void OnTriggerEnter(Collider hit){
		if(hit.tag == "Enemy"){
			health = hit.transform.parent.GetComponent<Health>();
			health.TakeDamage(damage);
			Kill();
		}
		if(hit.tag == "Player"){
			health = hit.transform.parent.GetComponent<Health>();
			health.TakeDamage(damage);
			Kill();
		}
		if(hit.tag == "Ground"){
			Kill();
		}
		if(hit.tag == "GroundEnemy"){
			Debug.Log("Hit");
			health = hit.transform.parent.GetComponent<Health>();
			health.TakeDamage(damage);
			Kill();
		}
		if(hit.tag == "Shield"){
			health = hit.transform.GetComponent<Health>();
			health.TakeDamage(damage);
			if(health.getHealth() > 0){
				hit.renderer.enabled = true;
				hit.collider.enabled = true;
			} else {
				hit.collider.enabled = false;
			}
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
	
	public void CanShoot(bool shoot){
		canShoot = shoot;
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
