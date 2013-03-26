using UnityEngine;
using System.Collections;

public class Kamikaze : BaseEnemy {
	
	public override void Start(){
		health.ModifyHealth(20f);
		ModifySpeed(30f);
		ModifyHeight(15f);
		base.Start();
	}
	
	public override void EnemyAttack(){
		ClampLookTime();
		if(target){
			if(curLookTime > 0){
				curLookTime -= Time.deltaTime;
				trans.position = new Vector3(trans.position.x, getFixedHeight(), Mathf.Lerp(trans.position.z, top-1, 2f*Time.deltaTime));
				// Rotate to look at the player
	   			trans.rotation = Quaternion.Slerp(trans.rotation,
	    			Quaternion.LookRotation(target.position-trans.position), 1);
			}
			if(curLookTime <= 0){
				// Move towards the player
		    	trans.position += trans.forward * getSpeed() * Time.deltaTime;
			}
		}
		base.EnemyAttack();
	}
}
