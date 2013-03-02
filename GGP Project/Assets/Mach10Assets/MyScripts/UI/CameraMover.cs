using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour {
	public Transform target;
	
	// Update is called once per frame
	void Update(){
		if(target){
			Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, target.position, 3*Time.deltaTime);
			Vector3 moveDirection = target.position - Camera.main.transform.position;
			if(moveDirection.sqrMagnitude < 0.001f){
				Camera.main.transform.position = target.position;
			}
		}
	}
}