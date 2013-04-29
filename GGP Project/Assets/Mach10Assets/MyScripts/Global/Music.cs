using UnityEngine;

public class Music : MonoBehaviour {
	
	public AudioClip music;
	private LevelWin levelWin;
	private GameOver gameOver;

	// Use this for initialization
	void Start(){
		levelWin = GameObject.Find("LevelWin").GetComponent<LevelWin>();
		gameOver = GameObject.Find("GameOver").GetComponent<GameOver>();
		audio.clip = music;
		audio.loop = true;
		audio.Play();
	}
	
	// Update is called once per frame
	void Update(){
		if(levelWin.getLevelWon() || gameOver.getGameOver()){
			audio.Stop();
		}
	}
}
