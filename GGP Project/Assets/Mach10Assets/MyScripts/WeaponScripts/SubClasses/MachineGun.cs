using UnityEngine;
using System.Collections;

public class MachineGun : Weapon {

	// Use this for initialization
	public override void Start(){
		ModifyFireRate(0.05f);
		ModifyConeAngle(1.5f);
	}
}
