using UnityEngine;
using System.Collections;

public class PlayerWeapon : MonoBehaviour {
	public enum BulletType {
		BULLET,
		ROCKET,
		HOMINGMISSILE,
		SHOTGUN,
		BOMB
	}
	public BulletType type;
	public float range = 100.0f;
	private float fireRate = 0.0f;
	private float nextFireTime = 0.0f;
	private float coneAngle = 1.5f;
	public Rigidbody bulletPrefab;
	public Rigidbody rocketPrefab;
	public Rigidbody bombPrefab;
	public Transform muzzlePos;
	public Transform bombPos;
	private Bullet bullet;
	private int roundsPerBurst = 0;
	private float timer = 0.2f;

	// Use this for initialization
	void Start(){
	
	}
	
	// Update is called once per frame
	void Update(){
		switch(type){
		case BulletType.BULLET:
			if(Input.GetKey(KeyCode.Space)){
				FireBullet();
			}
			break;
		case BulletType.ROCKET:
			if(Input.GetKey(KeyCode.Space)){
				FireRocket();
			}
			break;
		case BulletType.HOMINGMISSILE:
			if(Input.GetKey(KeyCode.Space)){
				FireHomingMissile();
			}
			break;
		case BulletType.SHOTGUN:
			if(Input.GetKey(KeyCode.Space)){
				FireShotGun();
			}
			break;
		case BulletType.BOMB:
			if(Input.GetKey(KeyCode.LeftShift)){
				DropBomb();
			}
			break;
		}
	}
	
	void FireBullet(){
		ModifyFireRate(0.05f);
		// If there is more than one bullet between the last frame and this frame
		// Reset the nextFireTime
		if(Time.time - fireRate > nextFireTime){
			nextFireTime = Time.time - Time.deltaTime;
		}
		
		// Keep firing until we used up the fire time
		while(nextFireTime < Time.time){
			CreateProjectile();
			nextFireTime += fireRate;
		}
	}
	
	void FireRocket(){
		ModifyFireRate(0.5f);
		if(Time.time - fireRate > nextFireTime) {
			nextFireTime = Time.time - Time.deltaTime;
		}
		
		while(nextFireTime < Time.time){
			CreateProjectile();
			nextFireTime += fireRate;
		}
	}
	
	void FireHomingMissile(){
	}
	
	void FireShotGun(){
		ModifyFireRate(0.5f);
		roundsPerBurst = 8;
		int pellets = 0;
		if(Time.time - fireRate > nextFireTime){
			nextFireTime = Time.time - Time.deltaTime;
		}
		
		if(Time.time > nextFireTime){
	    	while(pellets < roundsPerBurst){
				CreateProjectile();
		    	pellets++;
			}
			nextFireTime = Time.time + fireRate;
		}
	}
	
	void DropBomb(){
		ModifyFireRate(2f);
		if(Time.time - fireRate > nextFireTime){
			nextFireTime = Time.time - Time.deltaTime;
		}
		
		while(nextFireTime < Time.time){
			StartCoroutine("Wait");
			nextFireTime += fireRate;
		}
	}
	
	void CreateProjectile(){
		switch(type){
		case BulletType.BULLET:
			coneAngle = 1.5f;
			// Spawn visual bullet 
			Quaternion coneRandomRotation = 
				Quaternion.Euler(Random.Range(-coneAngle, coneAngle), Random.Range(-coneAngle, coneAngle), 0);
			Rigidbody visibleBullet = (Rigidbody)Instantiate(bulletPrefab, muzzlePos.position, muzzlePos.rotation * coneRandomRotation);
			bullet = visibleBullet.GetComponent<Bullet>();
			bullet.ModifyDamage(5f);
			bullet.ModifyLifeTime(0.3f);
			//RaycastForward();
			break;
		case BulletType.ROCKET:
			coneAngle = 2.0f;
			// Spawn visual bullet 
			coneRandomRotation = 
				Quaternion.Euler(Random.Range(-coneAngle, coneAngle), Random.Range(-coneAngle, coneAngle), 0);
			// Create a new projectile, use the same position and rotation as the Rocket Launcher.
			Rigidbody rocket = (Rigidbody)Instantiate(rocketPrefab, muzzlePos.position, muzzlePos.rotation * coneRandomRotation);
			bullet = rocket.GetComponent<Bullet>();
			bullet.ModifyDamage(5f);
			bullet.ModifyLifeTime(0.5f);
			//RaycastForward();
			break;
		case BulletType.SHOTGUN:
			coneAngle = 8.0f;
			coneRandomRotation = 
				Quaternion.Euler(Random.Range(-coneAngle, coneAngle), Random.Range(-coneAngle, coneAngle), 0);
			Rigidbody pellet = (Rigidbody)Instantiate(bulletPrefab, muzzlePos.position, muzzlePos.rotation * coneRandomRotation);
			bullet = pellet.GetComponent<Bullet>();
			bullet.ModifyDamage(10f);
			bullet.ModifyLifeTime(0.3f);
			//RaycastForward();
			break;
		case BulletType.BOMB:
			coneAngle = 5.0f;
			coneRandomRotation = 
				Quaternion.Euler(Random.Range(-coneAngle, coneAngle), Random.Range(-coneAngle, coneAngle), 0);
			Rigidbody bomb = (Rigidbody)Instantiate(bombPrefab, bombPos.position, bombPos.rotation * coneRandomRotation);
			bullet = bomb.GetComponent<Bullet>();
			bullet.ModifyDamage(20f);
			bullet.ModifyLifeTime(3f);
			//RaycastDown();
			break;
		}
	}
	
	/*void RaycastForward(){
		Vector3 direction = transform.TransformDirection(Vector3.forward);
	  	RaycastHit hit;
		// Bit shift the index of the layer 8 to get a bit mask
		LayerMask layermaskPlayer = 1<<8;
	  	// This would cast rays only against colliders in layer 8
	  	// But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
		layermaskPlayer = ~layermaskPlayer;
		
	  	// Does the ray intersect any objects excluding the player and fort layer
	  	if(Physics.Raycast(transform.position, direction, out hit, range, layermaskPlayer)){
			bullet.bulletDistance = hit.distance;
			Debug.DrawRay(transform.position, direction * hit.distance, Color.blue);
	  	} else {
			bullet.bulletDistance = 100f;
	    	Debug.DrawRay(transform.position, direction * range, Color.green);
	  	}
	}
	
	void RaycastDown(){
		Vector3 direction = transform.TransformDirection(Vector3.down);
		RaycastHit hit;
		// Bit shift the index of the layer 8 to get a bit mask
		LayerMask layermaskPlayer = 8;
		LayerMask layermaskGround = 10;
	  	// This would cast rays only against colliders in layer 8
	  	// But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
		LayerMask layermaskFinal = ~((1<<layermaskPlayer)|1<<layermaskGround);
		
	  	// Does the ray intersect any objects excluding the player and fort layer
	  	if(Physics.Raycast(transform.position, direction, out hit, range, layermaskFinal)){
			bullet.bulletDistance = hit.distance;
			Debug.DrawRay(transform.position, direction * hit.distance, Color.blue);
	  	} else {
			bullet.bulletDistance = 100f;
	    	Debug.DrawRay(transform.position, direction * range, Color.green);
	  	}
	}*/
	
	IEnumerator Wait(){
		roundsPerBurst = 3;
		for(int i=0; i<roundsPerBurst; i++){
			yield return new WaitForSeconds(timer);
			CreateProjectile();
		}
	}
	
	void SetState(BulletType bulletType){
		type = bulletType;
	}
	
	void ModifyFireRate(float amount){
		fireRate = amount;
	}
}
