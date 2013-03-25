using UnityEngine;

public class TurretProjectile : Weapon {
	
	// Use this for initialization
	public override void Start(){
		ModifyFireRate(Random.Range(1.5f, 3f));
	}
	
	// Update is called once per frame
	public override void Update(){
		if(canShoot){
			FireProjectile();
		}
	}
}
