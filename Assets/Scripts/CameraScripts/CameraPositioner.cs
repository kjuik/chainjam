using UnityEngine;
using System.Collections.Generic;

public class CameraPositioner : MonoBehaviour {

	public List<Player> players;
	public Camera cam;
	public float blockHeight = 1.6f;
	
	private float distanceToCameraTop;
	
	void Start(){
		float cameraTop = cam.ViewportToWorldPoint(new Vector3(0.5f,1f,0f)).y;
		distanceToCameraTop = cameraTop - this.transform.position.y;
	}
	
	void Update(){
		
			float max = this.transform.position.y + distanceToCameraTop;
		
			foreach(Player p in players){
				float playersTop = p.transform.position.y + p.gameObject.transform.lossyScale.y * blockHeight;
				
				if (playersTop > max)
					max = playersTop;
			}
			
			transform.Translate
				(0,(max - distanceToCameraTop)- this.transform.position.y,0);
		
	}
	
}
