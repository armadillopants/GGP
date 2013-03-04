using UnityEngine;
 
public class ShieldAnim : MonoBehaviour {
	public GameObject shield;
	public float scrollSpeed;
 
	private Material shieldMat;
	private Color color;
 
	void Start(){
		shieldMat = shield.renderer.material;
		shield.renderer.enabled = false;
		color = shield.renderer.material.color;
	}
 
	void LateUpdate(){
		if(shield.renderer.enabled){
			float offset = Time.deltaTime * scrollSpeed;
			shieldMat.SetFloat("_Offset", Mathf.Repeat (offset, 1.0f));
            color.a -= 0.01f;
            shield.renderer.material.color = color;
			if(shield.renderer.material.color.a <= 0f){
				shield.renderer.enabled = false;
			}
		}
		if(!shield.renderer.enabled){
			color.a = 1f;
			shield.renderer.material.color = color;
		}
	}
}