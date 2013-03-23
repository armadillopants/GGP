using UnityEngine;
using System.Collections;

public class TurretProjectile : Weapon {
	private float seconds = 0f;
	
	// Use this for initialization
	public override void Start(){
		ModifyFireRate(1.5f);
		seconds = Random.Range(2f, 4f);
	}
	
	// Update is called once per frame
	public override void Update(){
		seconds -= 1f*Time.deltaTime;
		if(canShoot && seconds<=0){
			FireProjectile();
			seconds = Random.Range(2f, 4f);
		}
	}
}
