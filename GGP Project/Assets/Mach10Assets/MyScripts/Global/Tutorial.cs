using UnityEngine;

public class Tutorial : MonoBehaviour {
	public AudioClip[] clips;
	private int curClip;
	private bool nextClip = false;
	public GameObject p;
	private bool spawnP = true;
	EnemyManager manager;
	PowerUps powerUp;
	Boosts boost;
	GameObject[] enemies;
	public TextMesh text;

	// Use this for initialization
	void Start(){
		curClip = 0;
		nextClip = true;
		manager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
		GameObject shield = GameObject.Find("Shield");
		Health shieldHealth = shield.GetComponent<Health>();
		shieldHealth.SetTakeDamage(false);
	}
	
	// Update is called once per frame
	void Update(){
		if(nextClip){
			PlaySound();
		} else {
			CycleClips();
		}
		if(curClip == 12){
			LevelWin levelWin = GameObject.Find("LevelWin").GetComponent<LevelWin>();
			levelWin.LevelWon();
			return;
		}
	}

	void PlaySound(){
		if(!audio.isPlaying){
			audio.clip = clips[curClip];
			audio.Play();
			nextClip = false;
		}
	}

	void CycleClips(){
		switch(curClip){
		case 0:
			curClip++;
			nextClip = true;
			break;
		case 1:
			text.text = "Press WASD or ARROW Keys to Move";
			if(Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")){
				nextClip = true;
				curClip++;
			}
			break;
		case 2:
			text.text = "Press SPACE or Z Key to Shoot";
			if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z)){
				nextClip = true;
				curClip++;
			}
			break;
		case 3:
			text.text = "";
			manager.spawnTutAirEnemies = true;
			enemies = GameObject.FindGameObjectsWithTag("Enemy");
			if(enemies != null){
				foreach(GameObject enemy in enemies){
					powerUp = enemy.GetComponent<PowerUps>();
					powerUp.canDrop = false;
					boost = enemy.GetComponent<Boosts>();
					boost.canDrop = false;
				}
			}
			if(manager.enemiesSpawned >= manager.enemiesSpawnedPerLevel && manager.maxEnemiesOnScreen.Length <= 0){
				nextClip = true;
				curClip++;
				manager.spawnTutAirEnemies = false;
				manager.enemiesSpawned = 0;
			}
			break;
		case 4:
			text.text = "Press either Control Key or the X Key to Drop Bombs";
			if(Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.X)){
				nextClip = true;
				curClip++;
			}
			break;
		case 5:
			text.text = "";
			manager.spawnTutGroundEnemies = true;
			if(manager.enemiesSpawned >= manager.enemiesSpawnedPerLevel && manager.maxEnemiesOnScreen.Length <= 0){
				nextClip = true;
				curClip++;
				manager.spawnTutGroundEnemies = false;
				manager.enemiesSpawned = 0;
			}
			break;
		case 6:
			text.text = "Collect the Power-Up";
			if(spawnP){
				Instantiate(p, new Vector3(0, 15, 8), Quaternion.identity);
				spawnP = false;
			}
			nextClip = true;
			curClip++;
			break;
		case 7:
			text.text = "";
			nextClip = true;
			curClip++;
			break;
		case 8:
			nextClip = true;
			curClip++;
			break;
		case 9:
			nextClip = true;
			curClip++;
			break;
		case 10:
			nextClip = true;
			curClip++;
			break;
		case 11:
			curClip++;
			break;
		}
	}
}
