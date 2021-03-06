using UnityEngine;

public class PauseMenu : MonoBehaviour {
	private bool paused = false;
	private GameOver gameOver;
	private LevelWin levelWon;
	public Texture2D pauseTex;
	public AudioClip pauseSound;
	
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
				AudioSource.PlayClipAtPoint(pauseSound, transform.position, 1f);
			}
			if(paused){
				Time.timeScale = 0;
				StatsTracker.setStopper(true);
			} else {
				Time.timeScale = 1;
				StatsTracker.setStopper(false);
			}
		}
	}
	
	void OnGUI(){
		if(paused){
			GUIStyle style = new GUIStyle();
			style.fontSize = 24;
			GUIContent content = new GUIContent(pauseTex);
			Vector2 size = style.CalcSize(content);
			GUI.Label(new Rect(Screen.width / 2 - size.x / 2,
								Screen.height / 3,
							   	size.x, size.y),
					 			content, style);
			if(levelWon.curLevel != "Tutorial"){
				if(GUI.Button(new Rect(Screen.width / 2 - size.x / 5f,
									   Screen.height / 3 + size.y / 2.5f,
									   size.x / 2.5f,
									   size.y / 10), 
							"Resume")){
					paused = false;
				}
			} else {
				if(GUI.Button(new Rect(Screen.width / 2 - size.x / 5f,
									   Screen.height / 3 + size.y / 2.5f,
									   size.x / 2.5f,
									   size.y / 10), 
							"Skip Tutorial")){
					Score.ResetScore();
					StatsTracker.Reset();
					paused = false;
					Application.LoadLevel(levelWon.nextLevel);
				}
			}
			if(GUI.Button(new Rect(Screen.width / 2 - size.x / 5f,
								   Screen.height / 3 + size.y / 1.95f,
								   size.x / 2.5f,
								   size.y / 10),
						"Restart")){
				Score.ResetScore();
				StatsTracker.Reset();
				Application.LoadLevel(Application.loadedLevel);
			}
			if(GUI.Button(new Rect(Screen.width / 2 - size.x / 5f,
								   Screen.height / 3 + size.y / 1.62f,
								   size.x / 2.5f,
								   size.y / 10),
						"Main Menu")){
				paused = false;
				Score.ResetScore();
				StatsTracker.Reset();
				Application.LoadLevel("MainMenu");
			}
			if(GUI.Button(new Rect(Screen.width / 2 - size.x / 5f,
								   Screen.height / 3 + size.y / 1.38f,
								   size.x / 2.5f,
								   size.y / 10),
						"Quit")){
				Application.Quit();
			}
		}
	}
}
