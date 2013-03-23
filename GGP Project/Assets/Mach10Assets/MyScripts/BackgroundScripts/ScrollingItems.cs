using UnityEngine;

public class ScrollingItems : Scroller {
	private GameObject[] scrollItems;
	
	public override void Start(){
		scrollItems = GameObject.FindGameObjectsWithTag("Item");
		foreach(GameObject items in scrollItems){
			items.transform.FindChild("Stump").gameObject.SetActive(false);//renderer.enabled = false;
		}
		base.Start();
	}
	
	// Update is called once per frame
	void Update(){
		foreach(GameObject items in scrollItems){
			offset = Time.time * scrollSpeed;
			transform.position = new Vector3(0, 0, -offset);
			
			Vector3 currentPos = items.transform.position;
			
			if(currentPos.z <= down){
				currentPos.z = currentPos.z + top+top;
				wrapAround = true;
			}
			if(wrapAround){
				items.renderer.enabled = true;
				items.transform.FindChild("Stump").gameObject.SetActive(false);//.renderer.enabled = false;
				items.transform.position = currentPos;
			}
			wrapAround = false;
		}
	}
}
