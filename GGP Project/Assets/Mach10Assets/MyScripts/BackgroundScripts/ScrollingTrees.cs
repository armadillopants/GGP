using UnityEngine;

public class ScrollingTrees : Scroller {
	private GameObject[] trees;
	
	public override void Start(){
		trees = GameObject.FindGameObjectsWithTag("Tree");
		base.Start();
	}
	
	// Update is called once per frame
	void Update(){
		foreach(GameObject tree in trees){
			offset = Time.time * scrollSpeed;
			transform.position = new Vector3(0, 0, -offset);
			Vector3 currentPos = tree.transform.position;
			
			if(currentPos.z <= down){
				currentPos.z = currentPos.z + top+top;
				wrapAround = true;
			}
			if(wrapAround){
				tree.transform.position = currentPos;
			}
			wrapAround = false;
		}
	}
}
