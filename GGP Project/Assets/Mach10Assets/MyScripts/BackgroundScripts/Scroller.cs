using UnityEngine;

public class Scroller : MonoBehaviour {
	public enum ScrollType{
		BACKGROUND,
		ITEM,
		TARGET,
		OTHER
	}
	public ScrollType type;
	public float scrollSpeed;
	private float offset;
	private bool wrapAround;
	private GameObject[] scrolls;
	bool background = false;
	
	Camera cam;
	float distance;
	float top;
	float down;
	public float up = 1.25f;
	public float bottom = -0.2f;

	// Use this for initialization
	void Start(){
		cam = Camera.main;
		distance = Vector3.Dot(cam.transform.forward, transform.position-cam.transform.position);
		top = cam.ViewportToWorldPoint(new Vector3(0, up, distance)).z;
		down = cam.ViewportToWorldPoint(new Vector3(0, bottom, distance)).z;
		switch(type){
		case ScrollType.BACKGROUND:
			background = true;
			break;
		case ScrollType.ITEM:
			scrolls = GameObject.FindGameObjectsWithTag("Item");
			foreach(GameObject items in scrolls){
				items.transform.FindChild("Stump").gameObject.SetActive(false);
			}
			break;
		case ScrollType.TARGET:
			scrolls = GameObject.FindGameObjectsWithTag("Target");
			break;
		case ScrollType.OTHER:
			scrolls = GameObject.FindGameObjectsWithTag("Volcano");
			break;
		}
	}
	
	void Update(){
		if(background){
			ScrollBackGround();
		} else {
			ScrollItems();
		}
	}
	
	void ScrollBackGround(){
		offset = Time.time * scrollSpeed;
		renderer.material.mainTextureOffset = new Vector2(0, offset);
	}
	
	void ScrollItems(){
		foreach(GameObject items in scrolls){
			offset = Time.time * scrollSpeed;
			transform.position = new Vector3(0, 0, -offset);
			
			Vector3 currentPos = items.transform.position;
			
			if(currentPos.z <= down){
				currentPos.z = currentPos.z + top+top;
				wrapAround = true;
			}
			if(wrapAround){
				if(items.renderer != null){
					items.renderer.enabled = true;
					items.transform.FindChild("Stump").gameObject.SetActive(false);
				}
				items.transform.position = currentPos;
			}
			wrapAround = false;
		}
	}
}
