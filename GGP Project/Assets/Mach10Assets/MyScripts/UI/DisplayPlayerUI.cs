using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class DisplayPlayerUI : MonoBehaviour {
	private Health health;
	private Health shieldHealth;
	private Lives lives;
	private GameObject player;
	private GameObject shield;
	private EnemyManager manager;
	Rect box = new Rect(10, Screen.height/1.2f, 100, 20);
	private Texture2D healthBar;
	private Texture2D shieldBar;
	//public Material shieldMat;
	
	[XmlRootAttribute("PlayerStats")]
	public class PlayerStatsInfo{
		[XmlElementAttribute("health")]
		public float health;
		[XmlElementAttribute("shieldHealth")]
		public float shieldHealth;
		[XmlElementAttribute("lives")]
		public int lives;
	}
	
	void GetXML(){
		FileStream readFS = new FileStream("PlayerStats.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
		//PlayerStatsInfo info = (PlayerStatsInfo)SerializerObj.Deserialize(readFS);
		readFS.Close();
		//Debug.Log(info.health);
	}

	// Use this for initialization
	void Start(){
		TextAsset xmlData = new TextAsset();
		xmlData = (TextAsset)Resources.Load("PlayerStats.xml", typeof(TextAsset));
		XmlDocument doc = new XmlDocument();
		doc.LoadXml(xmlData.text);
		XmlNode firstNode = doc.FirstChild;
		Debug.Log(xmlData.name);
		Debug.Log(firstNode.InnerText);
		//Debug.Log(firstNode.Attributes);
		//XmlNodeList healthAdd = doc.GetElementsByTagName("health");
		//<?xml version="1.0" encoding="utf-8" ?>
		
		player = GameObject.Find("Player");
		health = player.GetComponent<Health>();
		//health.curHealth = float.Parse(firstNode.Attributes.GetNamedItem("health").Value);
		health.ModifyHealth(100f);
		
		shield = GameObject.FindGameObjectWithTag("Shield");
		shieldHealth = shield.GetComponent<Health>();
		shieldHealth.ModifyHealth(50f);
		
		lives = player.GetComponent<Lives>();
		lives.ModifyLives(3);
		manager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
		
		healthBar = new Texture2D(1, 1, TextureFormat.RGB24, false);
		healthBar.SetPixel(0, 0, Color.red);
		healthBar.Apply();
		
		shieldBar = new Texture2D(1, 1, TextureFormat.RGB24, false);
		shieldBar.SetPixel(0, 0, Color.green);
		shieldBar.Apply();
	}
	
	void Update(){
		if(lives.getLives() <= 0){
			manager.ClearEnemies();
		}
	}
	
	void OnGUI(){
		if(player){
			GUI.BeginGroup(box);
			{
				GUI.DrawTexture(new Rect(0, 0, box.width*health.getHealth()/health.getMaxHealth(), box.height), healthBar, ScaleMode.StretchToFill);
			}
			GUI.EndGroup();
			GUI.BeginGroup(box);
			{
				//Graphics.DrawTexture(new Rect(0, 0, box.width*shieldHealth.getHealth()/shieldHealth.getMaxHealth(), box.height), shieldBar, shieldMat);
				GUI.DrawTexture(new Rect(0, 0, box.width*shieldHealth.getHealth()/shieldHealth.getMaxHealth(), box.height), shieldBar, ScaleMode.StretchToFill);
			}
			GUI.EndGroup();
			//GUI.backgroundColor = Color.red;
			//GUI.Button(new Rect(10, 10, Screen.width/2 /(health.getMaxHealth()/health.getHealth()), 20), "Health: " + health.getHealth() + "/" + health.getMaxHealth());
			//GUILayout.Label("Health: " + health.getHealth());
			GUI.Box(new Rect(10, Screen.height/1.15f, 100, 20), "Lives: " + lives.getLives());
			GUI.Box(new Rect(10, Screen.height/1.1f, 100, 20), "Score: " + Score.getScore());
			//GUILayout.Label("Lives: " + lives.getLives());
			//GUILayout.Label("Shield: " + shieldHealth.getHealth());
			//GUILayout.Label("Score: " + Score.getScore());
		}
	}
}
