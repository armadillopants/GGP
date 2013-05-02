using UnityEngine;

public class Mover : MonoBehaviour {
	private Camera cam;
	Vector3 randMove;
	float distance;
	float down;

	// Use this for initialization
	void Start(){
		cam = Camera.main;
		distance = Vector3.Dot(cam.transform.forward, transform.position - cam.transform.position);
		down = cam.ViewportToWorldPoint(new Vector3(0, 0f, distance)).z;
		randMove = new Vector3(Random.Range(-0.1f, 0.1f), 0, 0);
	}
	
	// Update is called once per frame
	void Update(){
		transform.position -= (Vector3.forward*Random.Range(2f, 8f)*Time.deltaTime)+randMove;
		transform.Rotate(new Vector3(180*Time.deltaTime, 180*Time.deltaTime, 180*Time.deltaTime), Space.World);
		if(transform.position.z <= down){
			Destroy(gameObject);
		}
	}
}
