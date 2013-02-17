using UnityEngine;
using System.Collections;

public class HomingMissile : Weapon {
	// Use this for initialization
	public override void Start(){
		ModifyFireRate(0.3f);
		ModifyConeAngle(2f);
	}
}
