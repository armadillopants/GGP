using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	private float maxHealth = 0.0f;
	public float curHealth = 0.0f;
	private bool isPlayer = false;
	private PlayerMovement mover;
	private Lives lives;
	private GameObject player;

	// Use this for initialization
	void Start(){
		player = GameObject.Find("Player");
		if(gameObject.tag == "Player"){
			isPlayer = true;
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
				lives.TakeLives(1);
				mover.setClampPos(false);
				mover.ResetPlayerPos();
				ModifyHealth(100f);
			} else {
				Die();
			}
		}
	}
	
	public void Die(){
		Destroy(gameObject);
	}
}
