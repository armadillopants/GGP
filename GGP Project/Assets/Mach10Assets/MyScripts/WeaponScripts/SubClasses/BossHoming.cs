using UnityEngine;
using System.Collections;

public class BossHoming : Weapon {

	// Use this for initialization
	public override void Start(){
		ModifyFireRate(Random.Range(3f, 5f));
	}
	
	// Update is called once per frame
	public override void Update(){
		if(canShoot){
			FireProjectile();
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
			yield return new WaitForSeconds(0.3f);
			CreateProjectile();
		}
	}
}
