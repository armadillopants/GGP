using UnityEngine;

public class GroundTurret : BaseEnemy {

	// Use this for initialization
	public override void Start(){
		health.ModifyHealth(20f);
		ModifySpeed(6f);
		ModifyHeight(0.5f);
		weapon = GetComponent<TurretProjectile>();
		base.Start();
	}
	
	public override void EnemyAttack(){
		Vector3 startPoint = new Vector3(trans.position.x, getFixedHeight(), trans.position.z);
		Vector3 endPoint = new Vector3(trans.position.x, getFixedHeight(), down);
		//trans.rotation = Quaternion.Euler(0, 180, 0);
		Vector3 fixedHeight = target.position;
		fixedHeight.y = trans.position.y;
		trans.rotation = Quaternion.Slerp(trans.rotation, 
					Quaternion.LookRotation(fixedHeight-trans.position), getSpeed()*Time.deltaTime);
		if(target){
			// Move the ground turret along with the scrolling background
			trans.position = Vector3.MoveTowards(startPoint, endPoint, getSpeed()*Time.deltaTime);
			for(int i=0; i<turretBall.Length; i++){
				turretBall[i].rotation = Quaternion.Slerp(turretBall[i].rotation, 
					Quaternion.LookRotation(target.position-trans.position), getSpeed()*Time.deltaTime);
			}
		}
		base.EnemyAttack();
	}
}
