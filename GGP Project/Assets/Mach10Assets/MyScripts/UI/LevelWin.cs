using UnityEngine;

public class LevelWin : MonoBehaviour {
	private bool levelWon = false;
	public string curLevel = "";
	public string nextLevel = "";
	public AudioClip win;
	public Texture2D clearedTex;
	private bool playSound = true;
	private GameOver gameOver;
	
	void Start(){
		gameOver = GameObject.Find("GameOver").GetComponent<GameOver>();
	}
	
	public void LevelWon(){
		levelWon = true;
		if(playSound){
			AudioSource.PlayClipAtPoint(win, transform.position, 1f);
			StatsTracker.setStopper(true);
			playSound = false;
		}
		GameObject player = GameObject.Find("Player");
		PlayerMovement move = player.GetComponent<PlayerMovement>();
		move.LevelComplete();
	}
	
	public bool getLevelWon(){
		return levelWon;
	}
	
	void OnGUI(){
		if(levelWon && !gameOver.getGameOver()){
			GUIStyle style = new GUIStyle();
			style.fontSize = 24;
			GUIContent content = new GUIContent(clearedTex);
			Vector2 size = style.CalcSize(content);
			GUI.Label(new Rect(Screen.width / 2 - size.x / 2,
								Screen.height / 4,
							   	size.x, size.y),
					 			content, style);
			if(curLevel != "Level3"){
				if(GUI.Button(new Rect(Screen.width / 2 - size.x / 5f,
									   Screen.height / 3 + size.y / 13f,
									   size.x / 2.5f,
									   size.y / 20), 
							"Next Level")){
					StatsTracker.ResetTimer();
					StatsTracker.setStopper(false);
					StatsTracker.ResetEnemiesKilled();
					Application.LoadLevel(nextLevel);
				}
			} else {
				if(GUI.Button(new Rect(Screen.width / 2 - size.x / 5f,
									   Screen.height / 3 + size.y / 13f,
									   size.x / 2.5f,
									   size.y / 20), 
							"Survival")){
					Score.ResetScore();
					StatsTracker.ResetTimer();
					StatsTracker.setStopper(false);
					StatsTracker.ResetEnemiesKilled();
					Application.LoadLevel("Survival");
				}
			}
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
