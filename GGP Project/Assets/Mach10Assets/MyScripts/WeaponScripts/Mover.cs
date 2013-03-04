using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {
	float speed = 4f;
	private Camera cam;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update(){
		float distance = Vector3.Dot(cam.transform.forward, transform.position - cam.transform.position);
		float down = cam.ViewportToWorldPoint(new Vector3(0, 0.1f, distance)).z;
		transform.position -= Vector3.forward*Random.Range(speed/2,speed)*Time.deltaTime;
		transform.Rotate(new Vector3(180*Time.deltaTime, 180*Time.deltaTime, 180*Time.deltaTime), Space.World);
		if(transform.position.z <= down){
			Destroy(gameObject);
		}
	}
}
