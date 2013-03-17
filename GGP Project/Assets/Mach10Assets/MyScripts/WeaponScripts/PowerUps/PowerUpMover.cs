using UnityEngine;
using System.Collections;

public class PowerUpMover : Mover {
	public AudioClip powerUpSound;
	
	void OnTriggerEnter(Collider hit){
		if(hit.tag == "Player"){
			AudioSource.PlayClipAtPoint(powerUpSound, hit.transform.position, 0.8f);
			WeaponManager manager = GameObject.Find("WeaponManager").GetComponent<WeaponManager>();
			if(manager.getTimer() == false){
				manager.SortPowerUps();
			} else {
				manager.getWeapon().enabled = false;
				manager.getWeapon().audio.enabled = false;
				manager.SortPowerUps();
				manager.ResetCountDown();
			}
			Destroy(gameObject);
		}
	}
}
