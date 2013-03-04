using UnityEngine;
using System.Collections;

public class BoostsMover : Mover {

	void OnTriggerEnter(Collider hit){
		if(hit.tag == "Player"){
			BoostsManager manager = GameObject.Find("Player").GetComponent<BoostsManager>();
			manager.SortBoosts();
			Destroy(gameObject);
		}
	}
}
