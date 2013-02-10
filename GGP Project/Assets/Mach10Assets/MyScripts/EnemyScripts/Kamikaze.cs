using UnityEngine;
using System.Collections;

public class Kamikaze : BaseEnemy {
	private float speedAmount = 30f;
	private float healthAmount = 20f;

	// Use this for initialization
	public override void Start(){
		type = BaseEnemy.EnemyType.KAMIKAZE;
		ModifyHealth(healthAmount);
		ModifySpeed(speedAmount);
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update(){
		base.Update();
	}
}
