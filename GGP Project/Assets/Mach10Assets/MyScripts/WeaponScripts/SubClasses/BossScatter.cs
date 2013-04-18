using UnityEngine;

public class BossScatter : Weapon {

	// Use this for initialization
	public override void Start(){
		ModifyFireRate(Random.Range(2f, 4f));
	}
	
	// Update is called once per frame
	public override void Update(){
		if(canShoot){
			FireProjectile();
		}
	}
}
