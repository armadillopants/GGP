using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss : BaseEnemy {
	float pickOne;
	bool once = true;
	LevelWin levelWin;
	
	// Use this for initialization
	public override void Start(){
		levelWin = GameObject.Find("LevelWin").GetComponent<LevelWin>();
		switch(levelWin.curLevel){
		case "Level1":
			health.ModifyHealth(1000f);
			break;
		case "Level2":
			health.ModifyHealth(2000f);
			break;
		case "Level3":
			health.ModifyHealth(3000f);
			break;
		case "Survival":
			health.ModifyHealth(4000f);
			break;
		}
		ModifyHeight(15f);
		weapon = GetComponent<GunbotProjectile>();
		base.Start();
	}
	
	public override void EnemyAttack(){
		ClampLookTime();
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
				if(pickOne < 0.4f){
					// Move towards the player
			    	trans.position = new Vector3(Mathf.Lerp(trans.position.x, left-left/2, 2f*Time.deltaTime), 
						getFixedHeight(), trans.position.z);
				}
				if(pickOne > 0.6f){
					trans.position = new Vector3(Mathf.Lerp(trans.position.x, right-right/2, 2f*Time.deltaTime), 
						getFixedHeight(), trans.position.z);
				}
				if(pickOne <= 0.6f && pickOne >= 0.4f){
					trans.position = new Vector3(Mathf.Lerp(trans.position.x, left+right, 2*Time.deltaTime),
						getFixedHeight(), trans.position.z);
				}
				StartCoroutine("Wait");
			}
		}
		base.EnemyAttack();
	}
	
	void PickOne(){
		pickOne = Random.Range(0, 1f);
	}
	
	private IEnumerator Wait(){
		yield return new WaitForSeconds(2f);
		curLookTime = 3f;
	}
}
