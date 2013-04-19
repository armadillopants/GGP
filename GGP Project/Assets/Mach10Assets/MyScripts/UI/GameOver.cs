using UnityEngine;

public class GameOver : MonoBehaviour {
	private bool gameOver = false;
	private LevelWin levelWin;
	public AudioClip lose;
	public Texture2D gameOverTex;
	
	void Start(){
		levelWin = GameObject.Find("LevelWin").GetComponent<LevelWin>();
	}
	
	public void EndGame(){
		gameOver = true;
		StatsTracker.setStopper(true);
		AudioSource.PlayClipAtPoint(lose, transform.position, 1f);
	}
	
	public bool getGameOver(){
		return gameOver;
	}
	
	void OnGUI(){
		if(gameOver && !levelWin.getLevelWon()){
			GUIStyle style = new GUIStyle();
			style.fontSize = 24;
			GUIContent content = new GUIContent(gameOverTex);
			Vector2 size = style.CalcSize(content);
			GUI.Label(new Rect(Screen.width / 2 - size.x / 2,
								Screen.height / 4,
							   	size.x, size.y),
					 			content, style);
			if(GUI.Button(new Rect(Screen.width / 2 - size.x / 5f,
								   Screen.height / 3 + size.y / 7.2f,
								   size.x / 2.5f,
								   size.y / 20), 
						"Restart")){
				Score.ResetScore();
				StatsTracker.ResetTimer();
				StatsTracker.setStopper(false);
				StatsTracker.ResetEnemiesKilled();
				Application.LoadLevel(Application.loadedLevel);
			}
			if(GUI.Button(new Rect(Screen.width / 2 - size.x / 5f,
								   Screen.height / 3 + size.y / 5f,
								   size.x / 2.5f,
								   size.y / 20),
						"Main Menu")){
				Score.ResetScore();
				StatsTracker.ResetTimer();
				StatsTracker.setStopper(false);
				StatsTracker.ResetEnemiesKilled();
				Application.LoadLevel("MainMenu");
			}
			if(GUI.Button(new Rect(Screen.width / 2 - size.x / 5f,
								   Screen.height / 3 + size.y / 3.85f,
								   size.x / 2.5f,
								   size.y / 20),
						"Quit")){
				Application.Quit();
			}
			GUI.Label(new Rect(Screen.width / 2 - size.x / 3.5f,
								Screen.height / 3 + size.y / 2.4f,
								size.x,
								size.y / 2), 
						"Score: " + Score.getScore() + "\n\n" + 
						"Enemies Killed: " + StatsTracker.getEnemiesKilled() + "\n\n" +
						"Time Survived: " + Mathf.Round(StatsTracker.getTimer()*100f)/100f);
		}
	}	
}
