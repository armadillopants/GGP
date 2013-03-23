using UnityEngine;
//using System.Xml;

public class Health : MonoBehaviour {
	private float maxHealth = 0.0f;
	public float curHealth = 0.0f;
	private bool isPlayer = false;
	private bool isShield = false;
	private PlayerMovement mover;
	private Lives lives;
	private GameObject player;
	public GameObject playerExplosion;
	public GameObject enemyExplosion;
	PowerUps powerUp;
	Boosts boost;
	//XmlDocument doc;

	// Use this for initialization
	void Start(){
		//doc = new XmlDocument("directory path to xml document");
		//XmlNode firstNode = doc.FirstChild;
		//curHealth = float.Parse(firstNode.Attributes.GetNamedItem("health").Value);
		player = GameObject.Find("Player");
		if(gameObject.tag == "Player"){
			isPlayer = true;
		}
		if(gameObject.tag == "Shield"){
			isShield = true;
		}
		lives = player.GetComponent<Lives>();
		mover = player.GetComponent<PlayerMovement>();
		if(GameObject.FindGameObjectWithTag("Manager") != null){
			powerUp = GameObject.FindGameObjectWithTag("Enemy").GetComponent<PowerUps>();
			boost = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boosts>();
		}
	}
	
	public void ModifyHealth(float amount){
		maxHealth = amount;
		curHealth = maxHealth;
	}
	
	public void TakeDamage(float damage){
		curHealth = Mathf.Max(0f, curHealth-damage);
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
				if(powerUp){
					powerUp.DropPowerUp();
				}
				if(boost){
					boost.DropPowerUp();
				}
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
		if(gameObject.name == "Scorpion(Clone)"){
			Score.AddScore(150);
		}
		Destroy(gameObject);
	}
}
