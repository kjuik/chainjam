using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {

	public float shake = 0;
	
	public float shakeAmount = 1000f;
	
	public float decreaseFactor  = 0.0005f;
	
	public bool cumulativeEffect = false;
	 
	private Vector3 pos;
	
	private Vector3 firstPosition;
	
	void Start() {
		
		firstPosition = transform.localPosition;
	}
	
	void Update() {
		
		
		
		if (shake > 0) {
			
			Vector3 vector = Random.insideUnitSphere * shakeAmount * shake;	
				
			transform.localPosition = new Vector3(pos.x + vector.x, pos.y + vector.y, pos.z) ;
		
			shake -= Time.deltaTime * decreaseFactor;
		
		} else {
				
			shake = 0.0f;
			pos = transform.localPosition;
	
		}
	
	}
	
	public void Shake(){
		
		pos = transform.localPosition;
		
		if (cumulativeEffect){
			this.shake += 1f;
		} else {
			this.shake = 1f;
		}
		
	}
}
