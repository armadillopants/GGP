using UnityEngine;

public class Bullet : MonoBehaviour {
	private Transform trans;
	public GameObject explosion;
	public float bulletSpeed = 0.0f;
	public float lifeTime = 0.0f;
	public float damage = 0.0f;
	public GameObject target;
	public bool isHoming = false;
	public string enemyType = "";
	private float damp = 6.0f;
	private Health health;
	Camera cam;
	
	float distance;
	float top;
	float down;
	
	PowerUps powerUp;
	Boosts boost;

	// Use this for initialization
	void Start(){
		trans = transform;
		Invoke("Kill", lifeTime);
		cam = Camera.main;
		distance = Vector3.Dot(cam.transform.forward, trans.position - cam.transform.position);
		top = cam.ViewportToWorldPoint(new Vector3(0, 0.9f, distance)).z;
		down = cam.ViewportToWorldPoint(new Vector3(0, -0.3f, distance)).z;
	}
	
	// Update is called once per frame
	void Update(){
		if(trans.position.z >= top || trans.position.z <= down){
			Kill();
		}
		
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
	
	void OnTriggerEnter(Collider hit){
		if(hit.tag == "Enemy"){
			health = hit.transform.parent.GetComponent<Health>();
			health.TakeDamage(damage);
			if(health.getHealth() <= 0){
				powerUp = hit.transform.parent.FindChild("CollisionData").GetComponent<PowerUps>();
				boost = hit.transform.parent.FindChild("CollisionData").GetComponent<Boosts>();
				powerUp.DropPowerUp();
				boost.DropPowerUp();
				if(hit.transform.parent.name == "Kamikaze(Clone)"){
					Score.AddScore(50);
				} else if(hit.transform.parent.name == "Gov_GunBot(Clone)"){
					Score.AddScore(100);
				} else if(hit.transform.parent.name == "Gov_Sentry(Clone)"){
					Score.AddScore(20);
				} else if(hit.transform.parent.name == "Swarmer(Clone)"){
					Score.AddScore(5);
				}
			}
			if(trans.childCount > 0){
				GameObject col = trans.FindChild("Collision").gameObject;
				if(col){
					Destroy(col.gameObject);
				}
			}
			Explode();
			Kill();
		}
		if(hit.tag == "Player"){
			health = hit.transform.parent.GetComponent<Health>();
			health.TakeDamage(damage);
			if(trans.childCount > 0){
				GameObject col = trans.FindChild("Collision").gameObject;
				if(col){
					Destroy(col.gameObject);
				}
			}
			Explode();
			Kill();
		}
		if(hit.tag == "Ground"){
			Explode();
			Kill();
		}
		if(hit.tag == "GroundEnemy"){
			health = hit.transform.parent.GetComponent<Health>();
			health.TakeDamage(damage);
			Explode();
			Kill();
		}
		/*if(hit.tag == "Item"){
			hit.collider.renderer.enabled = false;
			hit.collider.transform.FindChild("Stump").gameObject.SetActive(true);
		}*/
		if(hit.tag == "Shield"){
			health = hit.transform.GetComponent<Health>();
			health.TakeDamage(damage);
			if(health.getHealth() > 0){
				hit.renderer.enabled = true;
			}
			if(trans.childCount > 0){
				GameObject col = trans.FindChild("Collision").gameObject;
				if(col){
					Destroy(col.gameObject);
				}
			}
			Explode();
			Kill();
		}
	}
	
	void Explode(){
		if(explosion != null){
			Instantiate(explosion, trans.position, trans.rotation);
		}
		// Stop emitting particles in any children
		ParticleEmitter emitter = GetComponentInChildren<ParticleEmitter>();
		if(emitter){
			emitter.emit = false;
		}

		// Detach children
		trans.DetachChildren();
	}
	
	void Kill(){
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
		enemyList = GameObject.FindGameObjectsWithTag(enemyType);
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
