using UnityEngine;
using System.Collections;

public class LevelWin : MonoBehaviour {
	private bool levelWon = false;
	public string nextLevel = "";
	
	public void LevelWon(){
		levelWon = true;
	}
	
	public bool getLevelWon(){
		return levelWon;
	}
	
	void OnGUI(){
		if(levelWon){
			GUIStyle style = new GUIStyle();
			style.fontSize = 60;
			GUIContent content = new GUIContent("LEVEL COMPLETED");
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
				Application.LoadLevel(nextLevel);
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
								   size.x / 2,
								   size.y / 2),
						"Quit")){
				Application.LoadLevel("MainMenu");
			}
		}
	}	
}