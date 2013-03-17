using UnityEngine;
using System.Collections;

public class TurretProjectile : Weapon {

	// Use this for initialization
	public override void Start(){
		ModifyFireRate(1.5f);
		ModifyConeAngle(1f);
	}
	
	// Update is called once per frame
	public override void Update(){
		if(canShoot){
			FireProjectile();
		}
	}
}
