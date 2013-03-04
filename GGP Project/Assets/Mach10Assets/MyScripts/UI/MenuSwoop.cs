using UnityEngine;
using System.Collections;

public class MenuSwoop : MonoBehaviour {
	private Renderer[] render;
	private BoxCollider[] boxCollider;
	PlayerSwoop swoop;
	float tColor = 0f;

	// Use this for initialization
	void Start () {
		GameObject player = GameObject.Find("MainMenuPlayer");
		swoop = player.GetComponent<PlayerSwoop>();
		render = GetComponentsInChildren<Renderer>();
		boxCollider = GetComponentsInChildren<BoxCollider>();
		transform.position = new Vector3(-3.452692f, 6.587327f, 19.71225f);
		transform.localEulerAngles = new Vector3(0, 349.2383f, 0);
		foreach(Renderer rend in render){
			rend.enabled = false;
		}
		foreach(BoxCollider boxCol in boxCollider){
			boxCol.collider.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(swoop.curWaypoint == 7){
			FadeIn();
		}
	}
	
	void FadeIn(){
		float fadeInTime = 1.0f;
		tColor += Time.deltaTime / fadeInTime;
		Vector3 dir = new Vector3(-1.300683f, 6.587327f, 8.389913f);
		transform.position = Vector3.Lerp(transform.position, dir, 2*Time.deltaTime);
		foreach(Renderer rend in render){
			rend.material.color = Color.Lerp(Color.clear, Color.black, tColor); 
			rend.enabled = true;
		}
		foreach(BoxCollider boxCol in boxCollider){
			boxCol.collider.enabled = true;
		}
	}
}
