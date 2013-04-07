using UnityEngine;
using System.Collections;

public class HomingBomb : Weapon {
	
	// Use this for initialization
	public override void Start(){
		ModifyFireRate(2f);
		ModifyConeAngle(5f);
		ModifyRoundsPerBurst(6);
	}
	
	public override void Update(){
		if(Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.X)){
			if(canShoot){
				FireProjectile();
			}
		}
	}
	
	public override void FireProjectile(){
		if(Time.time - fireRate > nextFireTime){
			nextFireTime = Time.time - Time.deltaTime;
		}
		
		while(nextFireTime < Time.time){
			StartCoroutine("Wait");
			nextFireTime += fireRate;
		}
	}
	
	private IEnumerator Wait(){
		for(int i=0; i<roundsPerBurst; i++){
			yield return new WaitForSeconds(0.2f);
			CreateProjectile();
		}
	}
}
