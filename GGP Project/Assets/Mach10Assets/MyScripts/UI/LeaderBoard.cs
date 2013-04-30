using UnityEngine;

public class LeaderBoard : MonoBehaviour {
	
	public TextMesh[] text;

	void Start(){
		if(PlayerPrefs.GetInt("Score1") > 1000){
			text[0].text = PlayerPrefs.GetInt("Score1").ToString();
		} else {
			text[0].text = "1000";
		}
		if(PlayerPrefs.GetInt("Score2") > 2000){
			text[1].text = PlayerPrefs.GetInt("Score2").ToString();
		} else {
			text[1].text = "2000";
		}
		if(PlayerPrefs.GetInt("Score3") > 3000){
			text[2].text = PlayerPrefs.GetInt("Score3").ToString();
		} else {
			text[2].text = "3000";
		}
		if(PlayerPrefs.GetInt("Score4") > 4000){
			text[3].text = PlayerPrefs.GetInt("Score4").ToString();
		} else {
			text[3].text = "4000";
		}
		if(PlayerPrefs.GetString("Time") != ""){
			text[4].text = PlayerPrefs.GetString("Time");
		} else {
			text[4].text = "02:00:00";
		}
	}
}
