using UnityEngine;
using System.Collections;

public class Lives : MonoBehaviour {
	private float startLives = 0;
	private float curLives = 0;
	private GameObject player;
	
	void Start(){
		player = GameObject.Find("Player");
	}
	
	public void ModifyLives(float amount){
		startLives = amount;
		curLives = startLives;
	}
	
	public void TakeLives(float howMany){
		curLives = Mathf.Max(0, curLives-howMany);
		if(curLives == 0){
			EndGame();
		}
	}
	
	public void AddLives(float howMany){
		curLives = Mathf.Min(Mathf.Infinity, curLives+howMany);
	}
	
	public float getLives(){
		return curLives;
	}
	
	public void EndGame(){
		GameOver gameOver = GameObject.Find("GameOver").GetComponent<GameOver>();
		gameOver.EndGame();
		Destroy(player);
	}
}
