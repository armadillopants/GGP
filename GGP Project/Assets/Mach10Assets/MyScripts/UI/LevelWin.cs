using UnityEngine;

public class LevelWin : MonoBehaviour {
	private bool levelWon = false;
	public string curLevel = "";
	public string nextLevel = "";
	private GameOver gameOver;
	
	void Start(){
		gameOver = GameObject.Find("GameOver").GetComponent<GameOver>();
	}
	
	public void LevelWon(){
		levelWon = true;
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
			if(GUI.Button(new Rect(Screen.width / 2 - size.x / 4,
								   Screen.height / 3 + size.y + 20,
								   size.x / 2,
								   size.y / 2), 
						"Next Level")){
				Score.ResetScore();
				Application.LoadLevel(nextLevel);
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
