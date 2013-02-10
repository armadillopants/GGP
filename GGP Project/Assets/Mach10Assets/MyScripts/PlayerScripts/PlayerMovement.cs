using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	private Transform trans;
	private Camera cam;
	private Vector3 moveDirection = Vector3.zero;
	public float playerSpeed = 10.0f;
	private float playerFixedHeight = 15f;
	private float playerRotation = 20f;
	public float coolDown = 0.5f;
	public int keyCounter = 1;
	public bool isRotating = false;
	public float curDodgeTime = 0f;
	private float minDodgeTime = 0f;
	private float maxDodgeTime = 3f;
	public bool isDodgingLeft;
	public bool isDodgingRight;

	// Use this for initialization
	void Start(){
		trans = transform;
		cam = Camera.mainCamera;
		curDodgeTime = maxDodgeTime;
	}
	
	// Update is called once per frame
	void Update(){
		moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
		moveDirection *= playerSpeed * Time.deltaTime;
		trans.Translate(moveDirection);
		
		// Clamps player position to screen boundaries
		ClampPosition();
		
		// Reset the euler angles whenever not rotating
		if(!isRotating){
			trans.localEulerAngles = new Vector3(0, 0, 0);
		}
		
		// For WASD and ARROW controls
		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
			isDodgingLeft = false;
			RotatePlayerRight();
		} else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
			isDodgingRight = false;
			RotatePlayerLeft();
		}
		
		if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
			if(coolDown > 0 && keyCounter == 1){
				isDodgingLeft = true;
			} else {
				coolDown = 0.5f;
				keyCounter++;
			}
		}
		if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
			if(coolDown > 0 && keyCounter == 1){
				isDodgingRight = true;
			} else {
				coolDown = 0.5f;
				keyCounter++;
			}
		}
		
		if(coolDown > 0){
			coolDown -= 1*Time.deltaTime;
		} else {
			coolDown = 0;
			keyCounter = 0;
		}
		
		if(isDodgingLeft && curDodgeTime > 0){
			curDodgeTime -= 2*Time.deltaTime;
			Dodge(180*Time.deltaTime, -8f);
		} else if(isDodgingRight && curDodgeTime > 0){
			curDodgeTime -= 2*Time.deltaTime;
			Dodge(-180*Time.deltaTime, 8f);
		} else {
			isDodgingLeft = false;
			isDodgingRight = false;
			isRotating = false;
			curDodgeTime += 0.5f*Time.deltaTime;
			ClampDodgeTime();
		}
		
		// For dodging controls
		/*if(Input.GetMouseButton(0) && curDodgeTime>0){
			curDodgeTime -= 2*Time.deltaTime;
			Dodge(180*Time.deltaTime, -8f);
		} else if(Input.GetMouseButton(1) && curDodgeTime>0){
			curDodgeTime -= 2*Time.deltaTime;
			Dodge(-180*Time.deltaTime, 8f);
		} else {
			isRotating = false;
			curDodgeTime += 0.5f*Time.deltaTime;
			ClampDodgeTime();
		}*/
	}
	
	void ClampPosition(){
		float distance = Vector3.Dot(cam.transform.forward, trans.position - cam.transform.position);
		
        float left = cam.ViewportToWorldPoint(new Vector3(0.1f, 0, distance)).x;
        float right = cam.ViewportToWorldPoint(new Vector3(0.9f, 0, distance)).x;
        float top = cam.ViewportToWorldPoint(new Vector3(0, 0.9f, distance)).z;
        float down = cam.ViewportToWorldPoint(new Vector3(0, 0.1f, distance)).z;
 
        Vector3 pos = trans.position;
		pos = new Vector3(Mathf.Clamp(trans.position.x, left, right), playerFixedHeight, Mathf.Clamp(trans.position.z, down, top));
        
		trans.position = pos;

	}
	
	void ClampDodgeTime(){
		curDodgeTime = Mathf.Clamp(curDodgeTime, minDodgeTime, maxDodgeTime);
	}
	
	void RotatePlayerRight(){
		isRotating = true;
		trans.localEulerAngles = new Vector3(0, 0, -playerRotation);
	}
	
	void RotatePlayerLeft(){
		isRotating = true;
		trans.localEulerAngles = new Vector3(0, 0, playerRotation);
	}
	
	void Dodge(float angle, float pos){
		isRotating = true;
		trans.Rotate(new Vector3(0, 0, angle),Space.World);
		float curPosX = trans.position.x;
		float curPosZ = trans.position.z;
		trans.localPosition = new Vector3(Mathf.Lerp(curPosX, pos, 2*Time.deltaTime), playerFixedHeight, curPosZ);
	}
}