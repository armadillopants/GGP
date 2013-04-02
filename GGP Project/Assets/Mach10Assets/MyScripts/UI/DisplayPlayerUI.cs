using UnityEngine;
using System.Xml;

public class DisplayPlayerUI : MonoBehaviour {
	private Health health;
	private Health shieldHealth;
	private Lives lives;
	private GameObject player;
	private GameObject shield;
	private EnemyManager manager;
	private Texture2D healthBar;
	private Texture2D shieldBar;
	public Texture2D playerHUD;
	public Texture2D[] playerLife;
	
	Rect healthBox;
	Rect liveBox;
	Rect scoreBox;
	Rect HUD;
	
	// Use this for initialization
	void Start(){
		TextAsset asset = new TextAsset();
		asset = (TextAsset)Resources.Load("PlayerStats", typeof(TextAsset));
		XmlDocument doc = new XmlDocument();
		doc.LoadXml(asset.text);
		XmlNode firstNode = doc.FirstChild;
		
		player = GameObject.Find("Player");
		health = player.GetComponent<Health>();
		health.SetMaxHealth(100f);
		health.curHealth = float.Parse(firstNode.Attributes.GetNamedItem("health").Value);
		//health.ModifyHealth(100f);
		
		shield = GameObject.FindGameObjectWithTag("Shield");
		shieldHealth = shield.GetComponent<Health>();
		shieldHealth.SetMaxHealth(50f);
		shieldHealth.curHealth = float.Parse(firstNode.Attributes.GetNamedItem("shieldHealth").Value);
		//shieldHealth.ModifyHealth(50f);
		
		lives = player.GetComponent<Lives>();
		lives.curLives = int.Parse(firstNode.Attributes.GetNamedItem("lives").Value);
		//lives.ModifyLives(3);
		manager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
		
		healthBar = new Texture2D(1, 1, TextureFormat.RGB24, false);
		healthBar.SetPixel(0, 0, Color.red);
		healthBar.Apply();
		
		shieldBar = new Texture2D(1, 1, TextureFormat.RGB24, false);
		shieldBar.SetPixel(0, 0, Color.green);
		shieldBar.Apply();
		
		HUD = new Rect(0, Screen.height-120, Screen.width, playerHUD.height);
		healthBox = new Rect(20, Screen.height-85, 135, 22);
		liveBox = new Rect(25, Screen.height-55, 135, 20);
		scoreBox = new Rect(25, Screen.height-30, 135, 20);
	}
	
	void Update(){
		if(lives.getLives() <= 0){
			manager.ClearEnemies();
		}
	}
	
	void OnGUI(){
		if(player){
			GUIx.DrawScaleTexture(HUD, playerHUD);
			GUI.BeginGroup(healthBox);
			{
				GUI.DrawTexture(new Rect(0, 0, 
					healthBox.width*health.getHealth()/health.getMaxHealth(), healthBox.height), healthBar, ScaleMode.StretchToFill);
			}
			GUI.EndGroup();
			GUI.BeginGroup(healthBox);
			{
				GUI.DrawTexture(new Rect(0, 0, 
					healthBox.width*shieldHealth.getHealth()/shieldHealth.getMaxHealth(), healthBox.height), shieldBar, ScaleMode.StretchToFill);
			}
			GUI.EndGroup();
			GUI.BeginGroup(liveBox);
			{
				for(int i=0; i<lives.getLives(); i++){
					if(lives.getLives() <=3){
						GUI.Label(new Rect(0, 0, liveBox.width, liveBox.height), "Lives: ");
						GUI.Label(new Rect(35+25*i, 0, liveBox.width, liveBox.height), playerLife[i]);
					}
				}
				if(lives.getLives() > 3){
					GUI.Label(new Rect(0, 0, liveBox.width, liveBox.height), "Lives: " + " \t\t X " + lives.getLives());
					GUI.Label(new Rect(35, 0, liveBox.width, liveBox.height), playerLife[0]);
				}
			}
			GUI.EndGroup();
			GUI.BeginGroup(scoreBox);
			{
				GUI.Label(new Rect(0, 0, scoreBox.width, scoreBox.height), "Score: " + Score.getScore());
			}
			GUI.EndGroup();
		}
	}
}
