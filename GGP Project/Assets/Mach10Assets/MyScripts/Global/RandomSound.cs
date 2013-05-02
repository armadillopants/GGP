using UnityEngine;

public class RandomSound : MonoBehaviour {
	public AudioClip[] sounds;
	private int curSound;

	// Use this for initialization
	void Start(){
		curSound = Random.Range(0, sounds.Length);
		AudioSource.PlayClipAtPoint(sounds[curSound], transform.position, 0.3f);
	}
}
