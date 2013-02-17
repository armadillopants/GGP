using UnityEngine;
using System.Collections;

public class GovGunbot : BaseEnemy {

	// Use this for initialization
	public override void Start(){
		health = GetComponent<Health>();
		health.ModifyHealth(20f);
		ModifySpeed(3f);
		ModifyHeight(15f);
		base.Start();
	}
	
	public override void EnemyAttack(){
		Vector3 startPoint = new Vector3(trans.position.x, getFixedHeight(), trans.position.z);
		Vector3 endPoint = new Vector3(trans.position.x, getFixedHeight(), down);
		if(target){
			trans.position = Vector3.MoveTowards(startPoint, endPoint, getSpeed()*Time.deltaTime);
		}
		base.EnemyAttack();
	}
}
