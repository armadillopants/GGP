using UnityEngine;

public class Lives : MonoBehaviour {
	private int startLives = 0;
	public int curLives = 0;
	private GameObject player;
	private GameObject reticule;
	
	void Start(){
		player = GameObject.Find("Player");
		reticule = GameObject.Find("Reticule");
	}
	
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
	
	public void AddLives(int howMany){
		curLives = Mathf.Min(10, curLives+howMany);
	}
	
	public int getLives(){
		return curLives;
	}
	
	public void EndGame(){
		GameOver gameOver = GameObject.Find("GameOver").GetComponent<GameOver>();
		gameOver.EndGame();
		Destroy(player);
		Destroy(reticule);
	}
}
