using UnityEngine;

public class StatsTracker {
	private static int enemiesKilled;
	private static float enemiesSpawned;
	public static float timer;
	private static bool stopTimer;
	
	void Start(){
		enemiesKilled = 0;
		enemiesSpawned = 0;
		timer = 0;
		stopTimer = false;
	}
	
	public static int getEnemiesKilled(){
		return enemiesKilled;
	}
	
	public static float getEnemiesSpawned(){
		return enemiesSpawned;
	}
	
	public static float getTimer(){
		return timer;
	}
	
	public static bool getStopper(){
		return stopTimer;
	}
	
	public static void Reset(){
		enemiesKilled = 0;
		timer = 0;
		stopTimer = false;
	}
	
	public static void setStopper(bool time){
		stopTimer = time;
	}
	
	public static void AddEnemyKilled(int amount){
		enemiesKilled += amount;
	}
	
	public static void AddEnemySpawn(float amount){
		enemiesSpawned = amount;
	}
	
	public static string GuiTime(float time){
		float guiTime = time;
		int minutes = (int)(guiTime / 60); // Creates 00 for minutes
		int seconds = (int)(guiTime % 60); // Creates 00 for seconds
		int fraction = (int)(time * 100); // Creates 000 for fractions
		fraction = fraction % 100;
		string text = ""; // For displaying the the timer in min, sec, frac
	    text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
	    return text;
	}
}
