using UnityEngine;
using System.Collections;

public class Rocket : Weapon {
	// Use this for initialization
	public override void Start(){
		ModifyFireRate(0.5f);
		ModifyConeAngle(2f);
	}
}
