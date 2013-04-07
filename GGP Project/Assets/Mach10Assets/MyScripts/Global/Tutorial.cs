using UnityEngine;

public class Tutorial : MonoBehaviour {
	public AudioClip[] clips;
	private int curClip;
	private bool nextClip = false;

	// Use this for initialization
	void Start(){
		curClip = 0;
	}
	
	// Update is called once per frame
	void Update(){
		if(nextClip){
			PlaySound();
		}
		if(curClip == 12){
			return;
		} else if(!audio.isPlaying){
			audio.clip = clips[curClip];
			nextClip = true;
		}
	}

	void PlaySound(){
		audio.Play();
		curClip++;
		nextClip = false;
	}
}
