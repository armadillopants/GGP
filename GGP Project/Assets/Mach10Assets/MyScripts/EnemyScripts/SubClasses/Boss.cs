using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss : BaseEnemy {
	public Weapon[] weapons;
	private float pickOne;
	private bool once = true;
	LevelWin levelWin;
	
	// Use this for initialization
	public override void Start(){
		levelWin = GameObject.Find("LevelWin").GetComponent<LevelWin>();
		switch(levelWin.curLevel){
		case "Level1":
			health.ModifyHealth(1000f);
			break;
		case "Level2":
			health.ModifyHealth(5f);
			break;
		case "Level3":
			health.ModifyHealth(3000f);
			break;
		case "Survival":
			health.ModifyHealth(4000f);
			break;
		}
		weapon = weapons[0];
		ModifyHeight(15f);
		ModifySpeed(30f);
		base.Start();
	}
	
	public override void EnemyAttack(){
		ClampLookTime();
		SwitchWeapon();
		if(target){
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
				if(Vector3.Distance(trans.position, target.position) < 15f){
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
		//base.EnemyAttack();
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
	}
}
