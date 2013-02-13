using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {
	public enum EnemyType {
		KAMIKAZE,
		GROUNDTURRET,
	}
	public EnemyType type;
	private float enemySpeed;
	private Transform target;
	private Transform trans;
	private Camera cam;
	private float enemyAirFixedHeight = 15f;
	private float enemyGroundFixedHeight = 2f;
	public GameObject projectile = null;
	public float curLookTime = 0f;
	private float maxLookTime = 3f;
	private float minLookTime = 0f;
	public float reloadTime = 1f;
	public float turnSpeed = 5f;
	public float firePauseTime = 0.25f;
	public Transform[] muzzlePosition;
	public Transform turretBall;
	private Health health;
	
	private float nextFireTime;
	private float nextMoveTime;

	// Use this for initialization
	public virtual void Start(){
		trans = transform;
		target = GameObject.Find("Player").transform;
		health = GetComponent<Health>();
		cam = Camera.mainCamera;
		curLookTime = maxLookTime;
	}
	
	// Update is called once per frame
	public virtual void Update(){
		switch(type){
		case EnemyType.KAMIKAZE:
			Kamikaze();
			break;
		case EnemyType.GROUNDTURRET:
			GroundTurret();
			break;
		}
	}
	
	void Kamikaze(){
		ClampLookTime();
		float distance = Vector3.Dot(cam.transform.forward, trans.position - cam.transform.position);
		if(target){
			if(curLookTime > 0){
				curLookTime -= Time.deltaTime;
				float top = cam.ViewportToWorldPoint(new Vector3(0, 0.9f, distance)).z;
				trans.position = new Vector3(trans.position.x, enemyAirFixedHeight, Mathf.Lerp(trans.position.z, top, 2f*Time.deltaTime));
				// Rotate to look at the player
		   		trans.rotation = Quaternion.Slerp(trans.rotation,
		    		Quaternion.LookRotation(target.position-trans.position), 1);
			}
			if(curLookTime <= 0){
				// Move towards the player
		    	trans.position += trans.forward * enemySpeed * Time.deltaTime;
				float down = cam.ViewportToWorldPoint(new Vector3(0, 0.1f, distance)).z;
				Vector3 pos = new Vector3(0, enemyAirFixedHeight, down);
				if(trans.position.z <= pos.z){
					health.Die();
				}
			}
		}
	}
	
	void GroundTurret(){
		float distance = Vector3.Dot(cam.transform.forward, trans.position - cam.transform.position);
		float down = cam.ViewportToWorldPoint(new Vector3(0, 0f, distance)).z;
		if(target){
			// Move the ground turret along with the scrolling background
			trans.position = new Vector3(trans.position.x, enemyGroundFixedHeight, Mathf.Lerp(trans.position.z, down, 0.3f*Time.deltaTime));
			if(Time.time >= nextMoveTime){
				turretBall.rotation = Quaternion.Slerp(turretBall.rotation, 
					Quaternion.LookRotation(target.position-trans.position), Time.deltaTime*turnSpeed);
			}
			if(Time.time >= nextFireTime){
				FireProjectile();
			}
			Vector3 pos = new Vector3(0, enemyGroundFixedHeight, down);
			if(trans.position.z <= pos.z + 0.5f){
				health.Die();
			}
		}
	}
	
	void OnTriggerEnter(Collider hit){
		if(hit.tag == "Player"){
			if(type == EnemyType.KAMIKAZE){
				Health health = target.GetComponent<Health>();
				health.TakeDamage(20f);
				this.health.Die();
			}
		}
	}
	
	
	void FireProjectile(){
		Physics.IgnoreLayerCollision(12, 11, true);
		nextFireTime = Time.time+reloadTime;
		nextMoveTime = Time.time+firePauseTime;
		
		foreach(Transform muzzlePos in muzzlePosition){
			GameObject visibleProj = (GameObject)Instantiate(projectile, muzzlePos.position, muzzlePos.rotation);
			Bullet bullet = visibleProj.GetComponent<Bullet>();
			bullet.ModifyLifeTime(2f);
			bullet.ModifyDamage(5f);
			bullet.ModifySpeed(10f);
		}
	}
	
	void ClampLookTime(){
		curLookTime = Mathf.Clamp(curLookTime, minLookTime, maxLookTime);
	}
	
	public void ModifySpeed(float amount){
		enemySpeed = amount;
	}
}