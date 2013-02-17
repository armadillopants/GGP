using UnityEngine;
using System.Collections;

public class GovSentry : BaseEnemy {

	// Use this for initialization
	public override void Start(){
		health = GetComponent<Health>();
		health.ModifyHealth(10f);
		ModifySpeed(5f);
		ModifyHeight(15f);
		base.Start();
	}
	
	public override void EnemyAttack(){
		Vector3 startPoint = new Vector3(trans.position.x, trans.position.y, trans.position.z);
		Vector3 endPoint = new Vector3(trans.position.x, trans.position.y, down);
		if(target){
			trans.position = Vector3.MoveTowards(startPoint, endPoint, getSpeed()*Time.deltaTime);
		}
		base.EnemyAttack();
	}
}
