using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {
	private Transform trans;
	private Camera cam;
	public GameObject reticule;
	private Vector3 moveDirection = Vector3.zero;
	public float playerSpeed = 20.0f;
	private float playerFixedHeight = 15f;
	private float playerRotation = 20f;
	Weapon[] weapon;
	EnemyManager manager;
	
	private bool canControl;
	public bool isRotating = false;
	private bool clampPosition = false;
	public bool isTut = false;
	private GameObject pad;
	
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
		manager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
		trans = transform;
		cam = Camera.mainCamera;
		LevelWin levelWin = GameObject.Find("LevelWin").GetComponent<LevelWin>();
		if(levelWin.curLevel != "Tutorial"){
			clampPosition = false;
			ResetPlayerPos();
		} else {
			isTut = true;
		}
		distance = Vector3.Dot(cam.transform.forward, trans.position - cam.transform.position);
		top = cam.ViewportToWorldPoint(new Vector3(0, 0.9f, distance)).z;
		down = cam.ViewportToWorldPoint(new Vector3(0, 0.1f, distance)).z;
		left = cam.ViewportToWorldPoint(new Vector3(0.04f, 0, distance)).x;
        right = cam.ViewportToWorldPoint(new Vector3(0.96f, 0, distance)).x;
		pad = GameObject.Find("Pad");
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
		
		if(!StatsTracker.getStopper()){
			StatsTracker.timer += Time.deltaTime;
		}

		// Reset the euler angles whenever not rotating
		if(!isRotating){
			trans.localEulerAngles = new Vector3(0, 0, 0);
		}
		
		reticule.transform.position = new Vector3(trans.position.x, reticule.transform.position.y, trans.position.z);
		
		if(isTut){
			StartCoroutine("Wait");
		}
		
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
		manager.DestroyEnemies();
	}
	
	public void LevelComplete(){
		canControl = false;
		isRotating = false;
		trans.position = Vector3.Lerp(trans.position, new Vector3(0, 0, down), 1f*Time.deltaTime);
	}
	
	void FlyOnScreen(){
		manager.canSpawnEnemies = false;
		canControl = false;
		Vector3 startPoint = new Vector3(trans.position.x, playerFixedHeight, trans.position.z);
		Vector3 endPoint = new Vector3(trans.position.x, playerFixedHeight, down);
		trans.position = Vector3.MoveTowards(startPoint, endPoint, (playerSpeed/2.5f)*Time.deltaTime);
		if(trans.position.z >= down){
			clampPosition = true;
			canControl = true;
			if(!GameObject.Find("Bee(Clone)")){
				manager.canSpawnEnemies = true;
			}
		}
	}
	
	void LandPad(){
		isTut = false;
		if(pad){
			pad.transform.position -= new Vector3(0, 0, 4)*Time.deltaTime;
			Vector3 startPoint = new Vector3(trans.position.x, playerFixedHeight, trans.position.z);
			Vector3 endPoint = new Vector3(trans.position.x, playerFixedHeight, top-top);
			trans.position = Vector3.MoveTowards(startPoint, endPoint, (playerSpeed/2f)*Time.deltaTime);
			if(!pad.renderer.IsVisibleFrom(cam)){
				Destroy(pad);
				canControl = true;
			}
		}
	}
	
	void RotatePlayer(){
		if(Input.GetAxisRaw("Horizontal") > 0){
			isRotating = true;
			trans.localEulerAngles = new Vector3(0, 0, -playerRotation);
		} else if(Input.GetAxisRaw("Horizontal") < 0){
			isRotating = true;
			trans.localEulerAngles = new Vector3(0, 0, playerRotation);
		} else {
			isRotating = false;
		}
	}
	
	public void setClampPos(bool clampStatus){
		clampPosition = clampStatus;
	}
	
	private IEnumerator Wait(){
		canControl = false;
		yield return new WaitForSeconds(5f);
		LandPad();
	}
}