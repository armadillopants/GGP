using UnityEngine;

public class RandomSound : MonoBehaviour {
	public AudioClip[] sounds;
	private AudioClip curSound;

	// Use this for initialization
	void Start(){
		curSound = sounds[Random.Range(0, sounds.Length)];
		AudioSource.PlayClipAtPoint(curSound, transform.position, 0.5f);
	}
}
