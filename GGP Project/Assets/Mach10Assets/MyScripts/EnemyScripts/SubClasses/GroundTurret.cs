using UnityEngine;
using System.Collections;

public class GroundTurret : BaseEnemy {

	// Use this for initialization
	public override void Start(){
		health = GetComponent<Health>();
		health.ModifyHealth(20f);
		ModifySpeed(10f);
		ModifyHeight(2f);
		base.Start();
	}
	
	public override void EnemyAttack(){
		Vector3 startPoint = new Vector3(trans.position.x, getFixedHeight(), trans.position.z);
		Vector3 endPoint = new Vector3(trans.position.x, getFixedHeight(), down);
		if(target){
			// Move the ground turret along with the scrolling background
			trans.position = Vector3.MoveTowards(startPoint, endPoint, getSpeed()*Time.deltaTime);
			turretBall.rotation = Quaternion.Slerp(turretBall.rotation, 
				Quaternion.LookRotation(target.position-trans.position), getSpeed()*Time.deltaTime);
		}
		base.EnemyAttack();
	}
}
