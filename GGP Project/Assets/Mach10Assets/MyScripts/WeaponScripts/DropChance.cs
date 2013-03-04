using UnityEngine;
using System.Collections;

public class DropChance : MonoBehaviour {
	public GameObject[] obj;
	private GameObject curObj;
	public float dropRate;
	
	public void DropPowerUp(){
		curObj = obj[Random.Range(0, obj.Length)];
		float dropChance = Random.Range(0, 1f);
		if(dropChance <= dropRate){
			Instantiate(curObj, transform.parent.position, Quaternion.identity);
		}
	}
}
