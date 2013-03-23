using UnityEngine;

public class BaseEnemy : MonoBehaviour {

	private float speed = 0f;
	protected Transform target;
	protected Transform trans;
	private Camera cam;
	private float fixedHeight = 0f;
	protected float curLookTime = 0f;
	private float maxLookTime = 3f;
	private float minLookTime = 0f;
	
	public Transform[] turretBall;
	protected Health health;
	protected Weapon weapon;
	protected float distance;
	protected float top;
	protected float down;
	
	void Awake(){
		health = GetComponent<Health>();
	}

	// Use this for initialization
	public virtual void Start(){
		trans = transform;
		target = GameObject.Find("Player").transform;
		cam = Camera.mainCamera;
		curLookTime = maxLookTime;
		distance = Vector3.Dot(cam.transform.forward, trans.position - cam.transform.position);
		top = cam.ViewportToWorldPoint(new Vector3(0, 0.9f, distance)).z;
		down = cam.ViewportToWorldPoint(new Vector3(0, 0f, distance)).z;
	}
	
	// Update is called once per frame
	void Update(){
		RenderEnemy();
		if(target != null){
			EnemyAttack();
		}
	}
	
	public virtual void EnemyAttack(){
		float left = cam.ViewportToWorldPoint(new Vector3(-0.2f, 0, distance)).x;
		float right = cam.ViewportToWorldPoint(new Vector3(1.2f, 0, distance)).x;
		if(trans.position.z < top && trans.position.z > down){
			if(weapon){
				weapon.CanShoot(true);
			}
		}
		if(trans.position.z <= down || trans.position.x <= left || trans.position.x >= right){
			Destroy(gameObject);
		}
	}
	
	void RenderEnemy(){
		Renderer data = transform.FindChild("CollisionData").renderer;
		if(!data.IsVisibleFrom(cam)){
			data.enabled = false;
		}
		if(data.IsVisibleFrom(cam)){
			data.enabled = true;
		}
	}
	
	public void ClampLookTime(){
		curLookTime = Mathf.Clamp(curLookTime, minLookTime, maxLookTime);
	}
	
	public void ModifyHeight(float height){
		fixedHeight = height;
	}
	
	public float getFixedHeight(){
		return fixedHeight;
	}
	
	public void ModifySpeed(float s){
		speed = s;
	}
	
	public float getSpeed(){
		return speed;
	}
}