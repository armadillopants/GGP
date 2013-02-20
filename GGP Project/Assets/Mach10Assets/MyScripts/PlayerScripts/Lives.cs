using UnityEngine;
using System.Collections;

public class Lives : MonoBehaviour {
	private int startLives = 0;
	public int curLives = 0;
	
	public void ModifyLives(int amount){
		startLives = amount;
		curLives = startLives;
	}
	
	public void TakeLives(int howMany){
		curLives = Mathf.Max(0, curLives-howMany);
		if(curLives == 0){
			EndGame();
		}
	}
	
	public void EndGame(){
		GameOver gameOver = GameObject.Find("GameOver").GetComponent<GameOver>();
		gameOver.EndGame();
	}
}
