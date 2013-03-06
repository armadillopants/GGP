using UnityEngine;
//using System.Xml;

public class Health : MonoBehaviour {
	private float maxHealth = 0.0f;
	private float curHealth = 0.0f;
	private bool isPlayer = false;
	private bool isShield = false;
	private PlayerMovement mover;
	private Lives lives;
	private GameObject player;
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
	}
	
	public void ModifyHealth(float amount){
		maxHealth = amount;
		curHealth = maxHealth;
	}
	
	public void TakeDamage(float damage){
		curHealth = Mathf.Max(0f, curHealth-damage);
		if(curHealth == 0){
			if(isPlayer){
				lives.TakeLives(1f);
				mover.setClampPos(false);
				mover.ResetPlayerPos();
				ModifyHealth(100f);
				Score.TakeScore(Random.Range(5, 15));
			} else if(isShield){
				curHealth = 0f;
			} else {
				PowerUps powerUp = GameObject.FindGameObjectWithTag("Enemy").GetComponent<PowerUps>();
				powerUp.DropPowerUp();
				Boosts boost = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boosts>();
				boost.DropPowerUp();
				Score.AddScore(Random.Range(5, 20));
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
	
	public void Die(){
		EnemyManager manager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
		manager.maxEnemiesOnScreen--;
		Destroy(gameObject);
	}
}
