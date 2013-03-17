using UnityEngine;

public class Scroller : MonoBehaviour {
	public float scrollSpeed;
	protected float offset;
	protected bool wrapAround;
	
	Camera cam;
	float distance;
	protected float top;
	protected float down;

	// Use this for initialization
	public virtual void Start () {
		cam = Camera.main;
		distance = Vector3.Dot(cam.transform.forward, transform.position-cam.transform.position);
		top = cam.ViewportToWorldPoint(new Vector3(0, 1.3f, distance)).z;
		down = cam.ViewportToWorldPoint(new Vector3(0, -0.2f, distance)).z;
	}
}
