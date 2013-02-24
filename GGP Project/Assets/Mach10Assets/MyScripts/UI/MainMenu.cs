using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public string levelName = "";
	public bool isQuit = false;
	public bool isLevelSelect = false;
	
	void OnMouseEnter(){
		renderer.material.color = Color.blue;
	}
	
	void OnMouseExit(){
		renderer.material.color = Color.black;
	}
	
	void OnMouseDown(){
		if(!isQuit && !isLevelSelect){
			Application.LoadLevel(levelName);
		} else if(isLevelSelect) {
		
		} else {
			Application.Quit();
		}
	}
}
