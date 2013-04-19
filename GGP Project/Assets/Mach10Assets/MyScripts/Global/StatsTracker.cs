using UnityEngine;

public class StatsTracker {
	private static int enemiesKilled;
	public static float timer;
	private static bool stopTimer;
	
	void Start(){
		enemiesKilled = 0;
		timer = 0;
		stopTimer = false;
	}
	
	public static int getEnemiesKilled(){
		return enemiesKilled;
	}
	
	public static float getTimer(){
		return timer;
	}
	
	public static bool getStopper(){
		return stopTimer;
	}
	
	public static void ResetEnemiesKilled(){
		enemiesKilled = 0;
	}
	
	public static void ResetTimer(){
		timer = 0;
	}
	
	public static void setStopper(bool time){
		stopTimer = time;
	}
	
	public static void AddEnemyKilled(int amount){
		enemiesKilled += amount;
	}
}
