using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	private bool paused = false;
	private GameOver gameOver;
	private LevelWin levelWon;

	// Use this for initialization
	void Start () {
		gameOver = GameObject.Find("GameOver").GetComponent<GameOver>();
		levelWon = GameObject.Find("LevelWin").GetComponent<LevelWin>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!gameOver.getGameOver() && !levelWon.getLevelWon()){
			if(Input.GetKeyDown(KeyCode.Escape)){
				paused = !paused;
			}
			if(paused){
				Time.timeScale = 0;
			} else {
				Time.timeScale = 1;
			}
		}
	}
	
	void OnGUI(){
		if(paused){
			GUIStyle style = new GUIStyle();
			style.fontSize = 60;
			GUIContent content = new GUIContent("PAUSED");
			Vector2 size = style.CalcSize(content);
			GUI.Label(new Rect(Screen.width / 2 - size.x / 2,
								Screen.height / 3,
							   	size.x, size.y),
					 			content, style);
			if(GUI.Button(new Rect(Screen.width / 2 - size.x / 4,
								   Screen.height / 3 + size.y + 20,
								   size.x / 2,
								   size.y / 2), 
						"Resume")){
				paused = false;
			}
			if(GUI.Button(new Rect(Screen.width / 2 - size.x / 4,
								   Screen.height / 3 + size.y * 2 + 5,
								   size.x / 2,
								   size.y / 2),
						"Restart")){
				Application.LoadLevel(Application.loadedLevel);
			}
			if(GUI.Button(new Rect(Screen.width / 2 - size.x / 4,
								   Screen.height / 3 + size.y * 2 + 50,
								   size.x / 2f,
								   size.y / 2),
						"Quit")){
				paused = false;
				Application.LoadLevel("MainMenu");
			}
		}
	}
}
