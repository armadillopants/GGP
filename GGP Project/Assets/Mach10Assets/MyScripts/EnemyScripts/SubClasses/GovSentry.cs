using UnityEngine;
using System.Collections;

public class GovSentry : BaseEnemy {

	// Use this for initialization
	public override void Start(){
		health.ModifyHealth(10f);
		ModifySpeed(Random.Range(3f, 5f));
		ModifyHeight(15f);
		base.Start();
	}
	
	public override void EnemyAttack(){
		Vector3 startPoint = new Vector3(trans.position.x, getFixedHeight(), trans.position.z);
		Vector3 endPoint = new Vector3(trans.position.x, getFixedHeight(), down);
		trans.rotation = Quaternion.Euler(0, 180, 0);
		if(target){
			trans.position = Vector3.MoveTowards(startPoint, endPoint, getSpeed()*Time.deltaTime);
		}
		base.EnemyAttack();
	}
}
