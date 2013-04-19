using UnityEngine;

public class Score {
	private static int score;
	
	void Start(){
		score = 0;
	}
	
	public static int getScore(){
		return score;
	}
	
	public static void ResetScore(){
		score = 0;
	}
	
	public static void AddScore(int amount){
		score += amount;
	}
	
	public static void TakeScore(int amount){
		score = Mathf.Max(0, score-amount);
	}
}
