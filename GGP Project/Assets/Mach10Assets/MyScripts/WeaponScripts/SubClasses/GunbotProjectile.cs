using UnityEngine;
using System.Collections;

public class GunbotProjectile : Weapon {

	// Use this for initialization
	public override void Start(){
		ModifyFireRate(2f);
	}
	
	// Update is called once per frame
	public override void Update(){
		if(canShoot){
			FireProjectile();
		}
	}
}
