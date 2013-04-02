using UnityEngine;

public class ViewFrustrum : MonoBehaviour {
	private Camera cam;
	private float distance;
	private float top;
	private float down;
	private float left;
	private float right;
	
	private float topZ = 0;
	private float downZ = 0;
	private float leftX = 0;
	private float rightX = 0;
	
	public void StartView(){
		cam = Camera.main;
		distance = Vector3.Dot(cam.transform.forward, transform.position-cam.transform.position);
	}
	
	public void SetTopZ(float t){
		topZ = t;
		top = cam.ViewportToWorldPoint(new Vector3(0, topZ, distance)).z;
	}
	
	public void SetDownZ(float d){
		downZ = d;
		down = cam.ViewportToWorldPoint(new Vector3(0, downZ, distance)).z;
	}
	
	public void SetLeftX(float l){
		leftX = l;
		left = cam.ViewportToWorldPoint(new Vector3(leftX, 0, distance)).x;
	}
	
	public void SetRightX(float r){
		rightX = r;
		right = cam.ViewportToWorldPoint(new Vector3(rightX, 0, distance)).x;
	}
	
	public float GetTopZ(){
		return top;
	}
	
	public float GetDownZ(){
		return down;
	}
	
	public float GetLeftX(){
		return left;
	}
	
	public float GetRightX(){
		return right;
	}
}
