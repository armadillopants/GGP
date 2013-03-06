using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
	private bool gameOver = false;
	
	public void EndGame(){
		gameOver = true;
	}
	
	public bool getGameOver(){
		return gameOver;
	}
	
	void OnGUI(){
		if(gameOver){
			GUIStyle style = new GUIStyle();
			style.fontSize = 60;
			GUIContent content = new GUIContent("GAME OVER");
			Vector2 size = style.CalcSize(content);
			GUI.Label(new Rect(Screen.width / 2 - size.x / 2,
								Screen.height / 3,
							   	size.x, size.y),
					 			content, style);
			if(GUI.Button(new Rect(Screen.width / 2 - size.x / 4,
								   Screen.height / 3 + size.y + 20,
								   size.x / 2,
								   size.y / 2), 
						"Restart")){
				Score.ResetScore();
				Application.LoadLevel(Application.loadedLevel);
			}
			if(GUI.Button(new Rect(Screen.width / 2 - size.x / 4,
								   Screen.height / 3 + size.y * 2 + 5,
								   size.x / 2,
								   size.y / 2),
						"Quit")){
				Application.LoadLevel("MainMenu");
			}
		}
	}	
}
