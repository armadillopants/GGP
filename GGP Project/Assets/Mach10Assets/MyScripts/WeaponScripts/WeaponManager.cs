using UnityEngine;

public class WeaponManager : MonoBehaviour {
	public Weapon[] weapons;
	private Weapon curWeapon;
	private float countDown;
	private bool startTimer = false;

	void Start () {
		curWeapon = weapons[0];
		countDown = Random.Range(5f, 15f);
	}
	
	void Update () {
		if(startTimer){
			CountDown();
		}
	}
	
	public bool getTimer(){
		return startTimer;
	}
	
	public Weapon getWeapon(){
		return curWeapon;
	}

	public void SortPowerUps(){
		curWeapon = weapons[Random.Range(1, weapons.Length)];
		curWeapon.enabled = true;
		curWeapon.audio.enabled = true;
		weapons[0].enabled = false;
		weapons[0].audio.enabled = false;
		startTimer = true;
	}
	
	void CountDown(){
		countDown -= Time.deltaTime;
		if(countDown <= 0){
			curWeapon.enabled = false;
			curWeapon.audio.enabled = false;
			weapons[0].enabled = true;
			weapons[0].audio.enabled = true;
			ResetCountDown();
			startTimer = false;
		}
	}
	
	public void ResetCountDown(){
		countDown = Random.Range(5f, 15f);
	}
}
