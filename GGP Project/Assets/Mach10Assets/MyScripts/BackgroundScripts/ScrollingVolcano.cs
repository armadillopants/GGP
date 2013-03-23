using UnityEngine;

public class ScrollingVolcano : Scroller {
	private GameObject[] volcanos;
	
	// Use this for initialization
	public override void Start () {
		volcanos = GameObject.FindGameObjectsWithTag("Volcano");
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		foreach(GameObject volcano in volcanos){
			offset = Time.time * scrollSpeed;
			transform.position = new Vector3(0, 0, -offset);
			Vector3 currentPos = volcano.transform.position;
			
			if(currentPos.z <= down){
				currentPos.z = currentPos.z + top+top;
				wrapAround = true;
			}
			if(wrapAround){
				volcano.transform.position = currentPos;
			}
			wrapAround = false;
		}
	}
}
