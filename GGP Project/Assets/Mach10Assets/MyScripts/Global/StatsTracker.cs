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
	
	public static string GuiTime(int time){
		int guiTime = time;
		int minutes = guiTime / 60; // Creates 00 for minutes
		int seconds = guiTime % 60; // Creates 00 for seconds
		int fraction = time * 100; // Creates 000 for fractions
		fraction = fraction % 100;
		string text = ""; // For displaying the the timer in min, sec, frac
	    text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
	    return text;
	}
}
