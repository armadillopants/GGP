using UnityEngine;

public class Weapon : MonoBehaviour {
	public GameObject[] projectile;
	public Transform[] muzzlePos;
	public float fireRate = 0.0f;
	protected float nextFireTime = 0.0f;
	public int roundsPerBurst = 0;
	public float coneAngle = 0.0f;
	private float lastFrameShot = -1f;
	protected Bullet bullet;
	protected bool canShoot = false;

	// Use this for initialization
	public virtual void Start(){
		
	}
	
	// Update is called once per frame
	public virtual void Update(){
		if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Z)){
			if(canShoot){
				FireProjectile();
			}
		}
	}
	
	void LateUpdate(){			
		// We shot this frame, enable the audio
		if(lastFrameShot == Time.frameCount){
			if(audio){
				if(!audio.isPlaying){
					audio.Play();
					audio.loop = true;
				}
			}
		} else {
			// Disable audio loop when not firing
			if(audio){
				audio.loop = false;
			}
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
	}
	
	public virtual void CreateProjectile(){
		Quaternion coneRandomRotation = 
			Quaternion.Euler(Random.Range(-coneAngle, coneAngle), Random.Range(-coneAngle, coneAngle), 0);
		// Spawn visual projectile
		for(int i=0; i<muzzlePos.Length; i++){
			if(projectile.Length == 1){
				GameObject proj = (GameObject)Instantiate(projectile[0], muzzlePos[i].position, muzzlePos[i].rotation * coneRandomRotation);
				bullet = proj.GetComponent<Bullet>();
			}
		}
		if(projectile.Length > 1){
			GameObject proj;
			proj = (GameObject)Instantiate(projectile[0], muzzlePos[0].position, muzzlePos[0].rotation * coneRandomRotation);
			proj = (GameObject)Instantiate(projectile[1], muzzlePos[1].position, muzzlePos[1].rotation * coneRandomRotation);
			proj = (GameObject)Instantiate(projectile[2], muzzlePos[2].position, muzzlePos[2].rotation * coneRandomRotation);
			bullet = proj.GetComponent<Bullet>();
		}
		lastFrameShot = Time.frameCount;
	}
	
	public void CanShoot(bool shoot){
		canShoot = shoot;
	}
	
	public void ModifyFireRate(float amount){
		fireRate = amount;
	}
}