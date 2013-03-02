using UnityEngine;
using System.Collections;

public class PowerUps : MonoBehaviour {
	public GameObject powerUp;
	private float dropRate = 0.15f;
	
	public void DropPowerUp(){
		float dropChance = Random.Range(0, 1f);
		if(dropChance <= dropRate){
			Instantiate(powerUp, transform.parent.position, Quaternion.identity);
		}
	}
}
