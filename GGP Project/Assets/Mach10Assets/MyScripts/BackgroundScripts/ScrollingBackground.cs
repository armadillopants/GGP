using UnityEngine;

public class ScrollingBackground : Scroller {
	
	//public Texture[] textures;
	//public float scrollSpeed = -0.1f;
	//public float changeInterval = 1f;
	
	/*void Update(){
		if(textures.Length == 0){
			return;
		}
		
		int index = (int)(Time.time / changeInterval);
		index %= textures.Length;
		renderer.material.mainTexture = textures[index];
	}*/
 
    void LateUpdate(){
		offset = Time.time * scrollSpeed;
		renderer.material.mainTextureOffset = new Vector2(0, offset);
    }
}
