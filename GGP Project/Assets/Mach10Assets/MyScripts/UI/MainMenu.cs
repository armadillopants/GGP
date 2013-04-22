using UnityEngine;

public class MainMenu : MonoBehaviour {
	public string levelName = "";
	private TextMesh text = new TextMesh();
	CameraMover mover;
	
	void Start(){
		text = GetComponent<TextMesh>();
		mover = GameObject.Find("Main Camera").GetComponent<CameraMover>();
	}
	
	void OnMouseEnter(){
		renderer.material.color = Color.blue;
	}
	
	void OnMouseExit(){
		renderer.material.color = Color.black;
	}
	
	void OnMouseDown(){
		if(text.text == "Play" || text.text == "Survival" || text.text == "Level 1" || text.text == "Level 2" || text.text == "Level 3"){
			Application.LoadLevel(levelName);
		} else if(text.text == "Select Level"){
			mover.target = GameObject.Find("LevelSelectTarget").transform;
		} else if(text.text == "Back"){
			mover.target = GameObject.Find("MainMenuTarget").transform;
			Settings.Reset();
		} else if(text.text == "Settings"){
			mover.target = GameObject.Find("SettingsTarget").transform;
		} else if(text.text == "Resolution"){
			Settings.SetResolution(true);
		} else if(text.text == "Quality"){
			Settings.SetQuality(true);
		} else {
			Application.Quit();
		}
	}
}
