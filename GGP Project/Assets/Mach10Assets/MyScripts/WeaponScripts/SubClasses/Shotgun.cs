using UnityEngine;

public class Shotgun : Weapon {
	
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
