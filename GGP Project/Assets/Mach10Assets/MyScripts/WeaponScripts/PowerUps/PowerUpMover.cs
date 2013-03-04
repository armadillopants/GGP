using UnityEngine;
using System.Collections;

public class PowerUpMover : Mover {
	
	void OnTriggerEnter(Collider hit){
		if(hit.tag == "Player"){
			WeaponManager manager = GameObject.Find("WeaponManager").GetComponent<WeaponManager>();
			if(manager.getTimer() == false){
				manager.SortPowerUps();
			} else {
				manager.getWeapon().enabled = false;
				manager.SortPowerUps();
				manager.ResetCountDown();
			}
			Destroy(gameObject);
		}
	}
}
