using UnityEngine;

public class Swarmer : BaseEnemy {

	public override void Start(){
		health.ModifyHealth(10f);
		ModifyHeight(15f);
		weapon = GetComponent<GunbotProjectile>();
		base.Start();
	}
	
	public override void EnemyAttack(){
		ClampLookTime();
		if(target){
			trans.rotation = Quaternion.Slerp(trans.rotation,
				Quaternion.LookRotation(target.position-trans.position), 1);
			if(curLookTime > 0){
				curLookTime -= Time.deltaTime;
				trans.position = new Vector3(trans.position.x, getFixedHeight(), 
					Mathf.Lerp(trans.position.z, top-top, Random.Range(1f, 2f)*Time.deltaTime));
			}
			if(curLookTime <= 0){
				trans.position = new Vector3(trans.position.x, getFixedHeight(), 
					Mathf.Lerp(trans.position.z, top+(top-Random.Range(top, top-2f)), Random.Range(1f, 2f)*Time.deltaTime));
			}
		}
		base.EnemyAttack();
	}
}
