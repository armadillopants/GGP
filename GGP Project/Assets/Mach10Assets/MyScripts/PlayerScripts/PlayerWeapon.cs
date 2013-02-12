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
		ModifyFireRate(0.8f);
		if(Time.time - fireRate > nextFireTime) {
			nextFireTime = Time.time - Time.deltaTime;
		}
		
		while(nextFireTime < Time.time){
			CreateProjectile();
			nextFireTime += fireRate;
		}
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
			bullet.ModifySpeed(80f);
			bullet.ModifyLifeTime(0.3f);
			break;
		case BulletType.ROCKET:
			coneAngle = 2.0f;
			coneRandomRotation = 
				Quaternion.Euler(Random.Range(-coneAngle, coneAngle), Random.Range(-coneAngle, coneAngle), 0);
			Rigidbody rocket = (Rigidbody)Instantiate(rocketPrefab, muzzlePos.position, muzzlePos.rotation * coneRandomRotation);
			bullet = rocket.GetComponent<Bullet>();
			bullet.ModifyDamage(5f);
			bullet.ModifySpeed(50f);
			bullet.ModifyLifeTime(0.5f);
			break;
		case BulletType.HOMINGMISSILE:
			coneAngle = 2.0f;
			coneRandomRotation = 
				Quaternion.Euler(Random.Range(-coneAngle, coneAngle), Random.Range(-coneAngle, coneAngle), 0);
			rocket = (Rigidbody)Instantiate(rocketPrefab, muzzlePos.position, muzzlePos.rotation * coneRandomRotation);
			bullet = rocket.GetComponent<Bullet>();
			bullet.ModifyDamage(5f);
			bullet.ModifySpeed(15f);
			bullet.ModifyLifeTime(2f);
			bullet.isHoming = true;
			break;
		case BulletType.SHOTGUN:
			coneAngle = 8.0f;
			coneRandomRotation = 
				Quaternion.Euler(Random.Range(-coneAngle, coneAngle), Random.Range(-coneAngle, coneAngle), 0);
			Rigidbody pellet = (Rigidbody)Instantiate(bulletPrefab, muzzlePos.position, muzzlePos.rotation * coneRandomRotation);
			bullet = pellet.GetComponent<Bullet>();
			bullet.ModifyDamage(10f);
			bullet.ModifySpeed(80f);
			bullet.ModifyLifeTime(0.3f);
			break;
		case BulletType.BOMB:
			coneAngle = 5.0f;
			coneRandomRotation = 
				Quaternion.Euler(Random.Range(-coneAngle, coneAngle), Random.Range(-coneAngle, coneAngle), 0);
			Rigidbody bomb = (Rigidbody)Instantiate(bombPrefab, bombPos.position, bombPos.rotation * coneRandomRotation);
			bullet = bomb.GetComponent<Bullet>();
			bullet.ModifyDamage(20f);
			bullet.ModifySpeed(-5f);
			bullet.ModifyLifeTime(3f);
			break;
		}
	}
	
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
