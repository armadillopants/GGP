using UnityEngine;

public class Health : MonoBehaviour {
	private float maxHealth = 0.0f;
	public float curHealth = 0.0f;
	private bool isPlayer = false;
	private bool isShield = false;
	private bool canTakeDamage = true;
	private PlayerMovement mover;
	private Lives lives;
	private GameObject player;
	public GameObject playerExplosion;
	public GameObject enemyExplosion;
	//PowerUps powerUp;
	//Boosts boost;

	// Use this for initialization
	void Start(){
		player = GameObject.Find("Player");
		if(gameObject.tag == "Player"){
			isPlayer = true;
		}
		if(gameObject.tag == "Shield"){
			isShield = true;
		}
		lives = player.GetComponent<Lives>();
		mover = player.GetComponent<PlayerMovement>();
		/*GameObject[] manager = GameObject.FindGameObjectsWithTag("Manager");
		foreach(GameObject m in manager){
			if(m != null){
				powerUp = m.GetComponentInChildren<PowerUps>();
				boost = m.GetComponentInChildren<Boosts>();
			}
		}*/
	}
	
	void Update(){
		if(isShield){
			if(curHealth > 0){
				collider.enabled = true;
			} else {
				collider.enabled = false;
				renderer.enabled = false;
			}
		}
	}
	
	public void SetMaxHealth(float amount){
		maxHealth = amount;
	}
	
	public void ModifyHealth(float amount){
		maxHealth = amount;
		curHealth = maxHealth;
	}
	
	public void SetTakeDamage(bool can){
		canTakeDamage = can;
	}
	
	public void TakeDamage(float damage){
		if(canTakeDamage){
			curHealth = Mathf.Max(0f, curHealth-damage);
		}
		if(curHealth == 0){
			if(isPlayer){
				lives.TakeLives(1);
				mover.setClampPos(false);
				mover.ResetPlayerPos();
				ModifyHealth(100f);
				Score.TakeScore(50);
				if(playerExplosion){
					Instantiate(playerExplosion, transform.position, Quaternion.identity);
				}
			} else if(isShield){
				curHealth = 0f;
			} else {
				Die();
			}
		}
	}
	
	public void AddHealth(float howMuch){
		curHealth = Mathf.Min(maxHealth, curHealth+howMuch);
	}
	
	public float getHealth(){
		return curHealth;
	}
	
	public float getMaxHealth(){
		return maxHealth;
	}
	
	public void Die(){
		if(enemyExplosion){
			Instantiate(enemyExplosion, transform.position, Quaternion.identity);
		}
		/*if(powerUp){
			powerUp.DropPowerUp();
		}
		if(boost){
			boost.DropPowerUp();
		}*/
		Destroy(gameObject);
	}
}
