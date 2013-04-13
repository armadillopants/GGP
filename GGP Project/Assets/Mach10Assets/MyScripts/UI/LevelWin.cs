using UnityEngine;

public class LevelWin : MonoBehaviour {
	private bool levelWon = false;
	public string curLevel = "";
	public string nextLevel = "";
	public AudioClip win;
	private bool playSound = true;
	private GameOver gameOver;
	
	void Start(){
		gameOver = GameObject.Find("GameOver").GetComponent<GameOver>();
	}
	
	public void LevelWon(){
		levelWon = true;
		if(playSound){
			AudioSource.PlayClipAtPoint(win, transform.position, 1f);
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
			style.fontSize = 30;
			GUIContent content = new GUIContent("LEVEL COMPLETED\n" + "\t\t\t\t\tScore: " + Score.getScore());
			Vector2 size = style.CalcSize(content);
			GUI.Label(new Rect(Screen.width / 2 - size.x / 2,
								Screen.height / 3,
							   	size.x, size.y),
					 			content, style);
			if(curLevel != "Level3"){
				if(GUI.Button(new Rect(Screen.width / 2 - size.x / 4,
									   Screen.height / 3 + size.y + 20,
									   size.x / 2,
									   size.y / 2), 
							"Next Level")){
					//Score.ResetScore();
					Application.LoadLevel(nextLevel);
				}
			} else {
				if(GUI.Button(new Rect(Screen.width / 2 - size.x / 4,
									   Screen.height / 3 + size.y + 20,
									   size.x / 2,
									   size.y / 2), 
							"Survival")){
					Score.ResetScore();
					Application.LoadLevel("Survival");
				}
			}
			if(GUI.Button(new Rect(Screen.width / 2 - size.x / 4,
								   Screen.height / 3 + size.y * 2 + 5,
								   size.x / 2,
								   size.y / 2), 
						"Restart")){
				Score.ResetScore();
				Application.LoadLevel(Application.loadedLevel);
			}
			if(GUI.Button(new Rect(Screen.width / 2 - size.x / 4,
								   Screen.height / 3 + size.y * 2 + 50,
								   size.x / 2,
								   size.y / 2),
						"Main Menu")){
				Score.ResetScore();
				Application.LoadLevel("MainMenu");
			}
			if(GUI.Button(new Rect(Screen.width / 2 - size.x / 4,
								   Screen.height / 3 + size.y * 2 + 100,
								   size.x / 2,
								   size.y / 2),
						"Quit")){
				Application.Quit();
			}
		}
	}	
}
