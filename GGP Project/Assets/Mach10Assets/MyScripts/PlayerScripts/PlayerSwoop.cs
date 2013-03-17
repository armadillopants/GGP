using UnityEngine;

public class PlayerSwoop : MonoBehaviour {
	public Transform[] wayPoints;
	public int curWaypoint = 0;
	public int totalWayPoints;
	private Transform trans;
	private float speed = 15f;
	private Vector3 target;
	private Vector3 moveDirection;

	// Use this for initialization
	void Start(){
		trans = transform;
		totalWayPoints = wayPoints.Length;
	}
	
	// Update is called once per frame
	void Update(){
		LookAtTarget();
	}
	
	void LookAtTarget(){
		if(curWaypoint == totalWayPoints){
			return;
		} else {
			target = wayPoints[curWaypoint].position;
			moveDirection = (target - trans.position);
			trans.rotation = Quaternion.Slerp(trans.rotation, Quaternion.LookRotation(moveDirection), 4*Time.deltaTime);
			if(moveDirection.sqrMagnitude < 1){
				curWaypoint++;
			}
		}
		trans.Translate(Vector3.forward*speed*Time.deltaTime);
	}
}
