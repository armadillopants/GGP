using UnityEngine;
using System.Collections;

public class ScrollingTargets : Scroller {
	private GameObject[] targets;

	// Use this for initialization
	public override void Start () {
		targets = GameObject.FindGameObjectsWithTag("Target");
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		foreach(GameObject target in targets){
			offset = Time.time * scrollSpeed;
			transform.position = new Vector3(0, 0, -offset);
			Vector3 currentPos = target.transform.position;
			
			if(currentPos.z <= down){
				currentPos.z = currentPos.z + top+top;
				wrapAround = true;
			}
			if(wrapAround){
				target.transform.position = currentPos;
			}
			wrapAround = false;
		}
	}
}
