using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public GameObject projectile;
	public Transform[] muzzlePos;
	protected float fireRate = 0.0f;
	protected float nextFireTime = 0.0f;
	protected int roundsPerBurst = 0;
	private float coneAngle = 0.0f;
	protected Bullet bullet;

	// Use this for initialization
	public virtual void Start(){
		
	}
	
	// Update is called once per frame
	public virtual void Update(){
		if(Input.GetKey(KeyCode.Space)){
			FireProjectile();
		}
	}
	
	public virtual void FireProjectile(){
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
		Physics.IgnoreLayerCollision(8, 9, true);
		Physics.IgnoreLayerCollision(12, 11, true);
		Physics.IgnoreLayerCollision(9, 11, true);
		Physics.IgnoreLayerCollision(9, 13, true);
	}
	
	public virtual void CreateProjectile(){
		// Spawn visual projectile
		for(int i=0; i<muzzlePos.Length; i++){
			Quaternion coneRandomRotation = 
				Quaternion.Euler(Random.Range(-coneAngle, coneAngle), Random.Range(-coneAngle, coneAngle), 0);
			GameObject proj = (GameObject)Instantiate(projectile, muzzlePos[i].position, muzzlePos[i].rotation * coneRandomRotation);
			bullet = proj.GetComponent<Bullet>();
		}
	}
	
	public void ModifyFireRate(float amount){
		fireRate = amount;
	}
	
	public void ModifyConeAngle(float amount){
		coneAngle = amount;
	}
	
	public void ModifyRoundsPerBurst(int amount){
		roundsPerBurst = amount;
	}
}