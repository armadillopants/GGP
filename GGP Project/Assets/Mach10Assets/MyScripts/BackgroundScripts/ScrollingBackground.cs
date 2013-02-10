using UnityEngine;
using System.Collections;

public class ScrollingBackground : MonoBehaviour {
	
	public Texture[] textures;
	public float scrollSpeed = -0.1f;
	public float changeInterval = 1f;
	
	/*void Update(){
		if(textures.Length == 0){
			return;
		}
		
		int index = (int)(Time.time / changeInterval);
		index %= textures.Length;
		renderer.material.mainTexture = textures[index];
	}*/
 
    void LateUpdate(){
		float offset = Time.time * scrollSpeed;
		renderer.material.mainTextureOffset = new Vector2(0, offset);
    }
}
