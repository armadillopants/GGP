using UnityEngine;
using System.Collections;

public class Shotgun : Weapon {
	// Use this for initialization
	public override void Start(){
		ModifyFireRate(0.5f);
		ModifyConeAngle(6f);
		ModifyRoundsPerBurst(8);
	}
	
	public override void FireProjectile(){
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
}
