using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	private Transform trans;
	private Camera cam;
	public GameObject reticule;
	private Vector3 moveDirection = Vector3.zero;
	public float playerSpeed = 10.0f;
	private float playerFixedHeight = 15f;
	private float playerRotation = 20f;
	Weapon[] weapon;
	
	public float coolDown = 0.5f;
	public int keyCounter = 1;
	
	public float curDodgeTime = 0f;
	private float minDodgeTime = 0f;
	private float maxDodgeTime = 3f;
	
	private bool canControl;
	public bool isRotating = false;
	public bool isDodgingLeft;
	public bool isDodgingRight;
	private bool clampPosition = false;
	
	private float distance;
	private float down;
	private float left;
	private float right;
	private float top;

	// Use this for initialization
	void Start(){
		GameObject ret = (GameObject)Instantiate(reticule, reticule.transform.position, Quaternion.identity);
		ret.name = reticule.name;
		GameObject wep = GameObject.Find("Weapons");
		weapon = wep.GetComponentsInChildren<Weapon>();
		canControl = true;
		trans = transform;
		cam = Camera.mainCamera;
		curDodgeTime = maxDodgeTime;
		clampPosition = true;
		distance = Vector3.Dot(cam.transform.forward, trans.position - cam.transform.position);
		top = cam.ViewportToWorldPoint(new Vector3(0, 0.9f, distance)).z;
		down = cam.ViewportToWorldPoint(new Vector3(0, 0.1f, distance)).z;
		left = cam.ViewportToWorldPoint(new Vector3(0.04f, 0, distance)).x;
        right = cam.ViewportToWorldPoint(new Vector3(0.96f, 0, distance)).x;
	}
	
	// Update is called once per frame
	void Update(){
		reticule = GameObject.Find("Reticule");
		// Clamps player position to screen boundaries
		if(clampPosition){
			ClampPositionToViewPort();
		} else {
			FlyOnScreen();
		}

		// Reset the euler angles whenever not rotating
		if(!isRotating){
			trans.localEulerAngles = new Vector3(0, 0, 0);
		}
		
		reticule.transform.position = new Vector3(trans.position.x, reticule.transform.position.y, trans.position.z);
		
		if(canControl){
			// For WASD and ARROW controls
			moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
			moveDirection *= playerSpeed * Time.deltaTime;
			trans.Translate(moveDirection);
			
			foreach(Weapon wep in weapon){
				wep.CanShoot(true);
			}
			
			// Rotate the player Z axis slightly when moving left or right
			RotatePlayer();
			
			// For dodging controls
			/*if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
				if(coolDown > 0 && keyCounter == 1){
					//isDodgingLeft = true;
				} else {
					coolDown = 0.5f;
					keyCounter++;
				}
			}
			if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
				if(coolDown > 0 && keyCounter == 1){
					//isDodgingRight = true;
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
			}*/
		} else {
			foreach(Weapon wep in weapon){
				wep.CanShoot(false);
			}
		}
	}
	
	void ClampPositionToViewPort(){
        Vector3 pos = trans.position;
		pos = new Vector3(Mathf.Clamp(trans.position.x, left, right), playerFixedHeight, Mathf.Clamp(trans.position.z, down, top));
        
		trans.position = pos;

	}
	
	public void ResetPlayerPos(){
		trans.position = new Vector3(trans.position.x, playerFixedHeight, down-15);
	}
	
	public void LevelComplete(){
		canControl = false;
		isRotating = false;
		trans.position = Vector3.Lerp(trans.position, new Vector3(0, 0, down), 1f*Time.deltaTime);
	}
	
	void FlyOnScreen(){
		GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Manager");
		foreach(GameObject enemy in enemyList){
			Destroy(enemy);
		}
		GameObject[] bulletList = GameObject.FindGameObjectsWithTag("Bullet");
		foreach(GameObject bullet in bulletList){
			Destroy(bullet);
		}
		canControl = false;
		Vector3 startPoint = new Vector3(trans.position.x, playerFixedHeight, trans.position.z);
		Vector3 endPoint = new Vector3(trans.position.x, playerFixedHeight, down);
		trans.position = Vector3.MoveTowards(startPoint, endPoint, (playerSpeed/3)*Time.deltaTime);
		if(trans.position.z >= down){
			clampPosition = true;
			canControl = true;
		}
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
		} else {
			isRotating = false;
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