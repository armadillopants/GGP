using UnityEngine;
using System.Collections;

public class GroundTurret : BaseEnemy {
	private float healthAmount = 20f;
	private float lifeTimeAmount = 2f;

	// Use this for initialization
	public override void Start(){
		type = BaseEnemy.EnemyType.GROUNDTURRET;
		ModifyHealth(healthAmount);
		EnemyBullet bullet = projectile.GetComponent<EnemyBullet>();
		bullet.ModifyLifeTime(lifeTimeAmount);
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
	}
}
