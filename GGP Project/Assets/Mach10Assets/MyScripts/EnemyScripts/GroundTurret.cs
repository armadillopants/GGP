using UnityEngine;
using System.Collections;

public class GroundTurret : BaseEnemy {
	private float healthAmount = 20f;

	// Use this for initialization
	public override void Start(){
		type = BaseEnemy.EnemyType.GROUNDTURRET;
		Health health = GetComponent<Health>();
		health.ModifyHealth(healthAmount);
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
	}
}
