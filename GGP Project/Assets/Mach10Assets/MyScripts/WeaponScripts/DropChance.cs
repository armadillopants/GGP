using UnityEngine;

public class DropChance : MonoBehaviour {
	public GameObject[] obj;
	private GameObject curObj;
	public float dropRate;
	public bool canDrop;
	
	void Start(){
		canDrop = true;
	}
	
	public void DropPowerUp(){
		curObj = obj[Random.Range(0, obj.Length)];
		float dropChance = Random.Range(0, 1f);
		if(dropChance <= dropRate){
			if(canDrop){
				GameObject droppedObj = (GameObject)Instantiate(curObj, transform.parent.position, Quaternion.identity);
				droppedObj.name = curObj.name;
			}
		}
	}
}
