using UnityEngine;

public class Settings : MonoBehaviour {
	private static bool displayQuality = false;
	private static bool displayResolution = false;
	private Vector2 scrollPos = Vector2.zero;
	
	public static void SetQuality(bool q){
		displayQuality = q;
		displayResolution = false;
	}
	
	public static void SetResolution(bool r){
		displayResolution = r;
		displayQuality = false;
	}
	
	public static void Reset(){
		displayQuality = false;
		displayResolution = false;
	}
	
	void OnGUI(){
		if(displayQuality){
	        string[] names = QualitySettings.names;
	        int i = 0;
			scrollPos = GUI.BeginScrollView(new Rect(Screen.width/2+100, Screen.height/2f, 100, 100), scrollPos, new Rect(0, 0, 64, 180));
	        while(i < names.Length){
	            if(GUILayout.Button(names[i])){
	                QualitySettings.SetQualityLevel(i, true);
				}
	            i++;
	        }
			GUI.EndScrollView();
		}
		
		if(displayResolution){
			Resolution[] resolutions = Screen.resolutions;
			int j = 0;
			scrollPos = GUI.BeginScrollView(new Rect(Screen.width/2+140, Screen.height/3, 100, 100), scrollPos, new Rect(0, 0, 64, 500));
	        while(j < resolutions.Length){
				if(GUILayout.Button(resolutions[j].width + "x" + resolutions[j].height)){
					Screen.SetResolution(resolutions[j].width, resolutions[j].height, true);
				}
				j++;
	        }
			GUI.EndScrollView();
		}
    }
}
