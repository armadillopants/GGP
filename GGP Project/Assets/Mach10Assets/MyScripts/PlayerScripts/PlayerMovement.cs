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
	private bool clampPosition = false;

	// Use this for initialization
	void Start(){
		trans = transform;
		cam = Camera.mainCamera;
		curDodgeTime = maxDodgeTime;
		clampPosition = true;
	}
	
	// Update is called once per frame
	void Update(){
		// For WASD and ARROW controls
		moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
		moveDirection *= playerSpeed * Time.deltaTime;
		trans.Translate(moveDirection);
		
		// Clamps player position to screen boundaries
		if(clampPosition){
			ClampPositionToViewPort();
		}
		
		// Rotate the player Z axis slightly when moving left or right
		RotatePlayer();
		
		// Reset the euler angles whenever not rotating
		if(!isRotating){
			trans.localEulerAngles = new Vector3(0, 0, 0);
		}
		
		// For dodging controls
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
		
		float distance = Vector3.Dot(cam.transform.forward, trans.position - cam.transform.position);
        float left = cam.ViewportToWorldPoint(new Vector3(0.1f, 0, distance)).x;
        float right = cam.ViewportToWorldPoint(new Vector3(0.9f, 0, distance)).x;
		
		if(isDodgingLeft && curDodgeTime > 0){
			if(!Input.GetKey(KeyCode.A)){
				curDodgeTime -= 2*Time.deltaTime;
				Dodge(180*Time.deltaTime, left);
			}
		} else if(isDodgingRight && curDodgeTime > 0){
			if(!Input.GetKey(KeyCode.D)){
				curDodgeTime -= 2*Time.deltaTime;
				Dodge(-180*Time.deltaTime, right);
			}
		} else {
			isDodgingLeft = false;
			isDodgingRight = false;
			isRotating = false;
			curDodgeTime += 0.5f*Time.deltaTime;
			ClampDodgeTime();
		}
	}
	
	void ClampPositionToViewPort(){
		float distance = Vector3.Dot(cam.transform.forward, trans.position - cam.transform.position);
		
        float left = cam.ViewportToWorldPoint(new Vector3(0.1f, 0, distance)).x;
        float right = cam.ViewportToWorldPoint(new Vector3(0.9f, 0, distance)).x;
        float top = cam.ViewportToWorldPoint(new Vector3(0, 0.9f, distance)).z;
        float down = cam.ViewportToWorldPoint(new Vector3(0, 0.1f, distance)).z;
 
        Vector3 pos = trans.position;
		pos = new Vector3(Mathf.Clamp(trans.position.x, left, right), playerFixedHeight, Mathf.Clamp(trans.position.z, down, top));
        
		trans.position = pos;

	}
	
	public void ResetPlayerPos(){
		trans.position = new Vector3(0, playerFixedHeight, -15);
	}
	
	void ClampDodgeTime(){
		curDodgeTime = Mathf.Clamp(curDodgeTime, minDodgeTime, maxDodgeTime);
	}
	
	void RotatePlayer(){
		if(Input.GetAxisRaw("Horizontal") > 0){
			isRotating = true;
			isDodgingLeft = false;
			trans.localEulerAngles = new Vector3(0, 0, -playerRotation);
		} else if(Input.GetAxisRaw("Horizontal") < 0){
			isRotating = true;
			isDodgingRight = false;
			trans.localEulerAngles = new Vector3(0, 0, playerRotation);
		}
	}
	
	void Dodge(float angle, float pos){
		isRotating = true;
		trans.Rotate(new Vector3(0, 0, angle),Space.World);
		float curPosX = trans.position.x;
		float curPosZ = trans.position.z;
		trans.localPosition = new Vector3(Mathf.Lerp(curPosX, pos, 1*Time.deltaTime), playerFixedHeight, curPosZ);
	}
	
	public void setClampPos(bool clampStatus){
		clampPosition = clampStatus;
	}
}