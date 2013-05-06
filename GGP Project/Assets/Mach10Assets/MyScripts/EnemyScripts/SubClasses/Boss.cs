using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss : BaseEnemy {
	public Weapon[] weapons;
	public AudioClip rush;
	private float pickOne;
	private float dist = 0f;
	private bool once = true;
	private bool charging = true;
	LevelWin levelWin;
	Rect healthBox;
	private Texture2D healthBar;
	private Texture2D greyBar;
	
	// Use this for initialization
	public override void Start(){
		levelWin = GameObject.Find("LevelWin").GetComponent<LevelWin>();
		switch(levelWin.curLevel){
		case "Level1":
			health.ModifyHealth(1000f);
			dist = 15f;
			break;
		case "Level2":
			health.ModifyHealth(2000f);
			dist = 17.5f;
			break;
		case "Level3":
			health.ModifyHealth(3000f);
			dist = 20f;
			break;
		case "Survival":
			health.ModifyHealth(4000f);
			dist = 20f;
			break;
		}
		weapon = weapons[0];
		ModifyHeight(15f);
		ModifySpeed(30f);
		healthBox = new Rect(Screen.width/2-150, Screen.height-(Screen.height-5), 300, 20);
		healthBar = new Texture2D(1, 1, TextureFormat.RGB24, false);
		healthBar.SetPixel(0, 0, Color.red);
		healthBar.Apply();
		greyBar = new Texture2D(1, 1, TextureFormat.RGB24, false);
		greyBar.SetPixel(0, 0, Color.grey);
		greyBar.Apply();
		base.Start();
	}
	
	public override void EnemyAttack(){
		ClampLookTime();
		SwitchWeapon();
		if(target){
			if(target.position.z < down){
				if(weapon){
					weapon.CanShoot(false);
				}
			} else {
				if(weapon){
					weapon.CanShoot(true);
				}
			}
			// Rotate to look at the player
	   		trans.rotation = Quaternion.Slerp(trans.rotation,
	    		Quaternion.LookRotation(target.position-trans.position), 1);
			if(curLookTime > 0){
				if(once){
					PickOne();
					once = false;
				}
				curLookTime -= Time.deltaTime;
				trans.position = new Vector3(trans.position.x, getFixedHeight(), Mathf.Lerp(trans.position.z, top-1, 2f*Time.deltaTime));
			}
			if(curLookTime <= 0){
				once = true;
				// Move towards player if too close
				if(Vector3.Distance(trans.position, target.position) < dist){
					trans.position += trans.forward*getSpeed()*Time.deltaTime;
					Quaternion rotate = Quaternion.LookRotation(target.transform.position - trans.position);
					trans.rotation = Quaternion.Slerp(trans.rotation, rotate, Time.deltaTime * 0.5f);
				}
				if(Vector3.Distance(trans.position, target.position) < 5f){
					ModifySpeed(-1f);
					trans.position += trans.forward*getSpeed()*Time.deltaTime;
				} else {
					ModifySpeed(30f);
				}
				if(charging){
					AudioSource.PlayClipAtPoint(rush, trans.position, 1f);
					charging = false;
				}
				// Move left or right randomly
				if(pickOne <= 0.45f){
			    	trans.position = new Vector3(Mathf.Lerp(trans.position.x, left-left/2, 2f*Time.deltaTime), 
						getFixedHeight(), trans.position.z);
				}
				if(pickOne >= 0.55f){
					trans.position = new Vector3(Mathf.Lerp(trans.position.x, right-right/2, 2f*Time.deltaTime), 
						getFixedHeight(), trans.position.z);
				}
				if(pickOne < 0.55f && pickOne > 0.45f){
					trans.position = new Vector3(Mathf.Lerp(trans.position.x, left+right, 2*Time.deltaTime),
						getFixedHeight(), trans.position.z);
				}
				StartCoroutine("Wait");
			}
		}
	}
	
	void SwitchWeapon(){
		if(levelWin.curLevel != "Level1"){
			if(health.getHealth() < health.getMaxHealth()/2){
				weapons[0].enabled = false;
				weapons[0].audio.enabled = false;
				weapon = weapons[1];
				weapon.enabled = true;
				weapon.audio.enabled = true;
			}
		}
		if(levelWin.curLevel == "Level3" || levelWin.curLevel == "Survival"){
			if(health.getHealth() < health.getMaxHealth()/3){
				weapon = weapons[2];
				weapon.enabled = true;
				weapon.audio.enabled = true;
			}
		}
	}
	
	void PickOne(){
		pickOne = Random.Range(0, 1f);
	}
	
	private IEnumerator Wait(){
		yield return new WaitForSeconds(1f);
		curLookTime = 3f;
		charging = true;
	}
	
	void OnGUI(){
		if(GameObject.Find("Bee(Clone)")){
			GUI.BeginGroup(healthBox);
			{
				GUI.DrawTexture(new Rect(0, 0, 
					healthBox.width*health.getMaxHealth(), healthBox.height), greyBar, ScaleMode.StretchToFill);
				GUI.DrawTexture(new Rect(0, 0, 
					healthBox.width*health.getHealth()/health.getMaxHealth(), healthBox.height), healthBar, ScaleMode.StretchToFill);
			}
			GUI.EndGroup();
		}
	}
}
