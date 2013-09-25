using UnityEngine;
using System.Collections.Generic;

public class PlayerKiller : MonoBehaviour {

	public List<Player> players;
	public Camera cam;
	public float blockHeight = -0.45f;
	
	void Update(){
		
			float cameraBottom = cam.ViewportToWorldPoint(new Vector3(0.5f,0f,0f)).y;
		
			foreach(Player p in players){
				float playersBottom = p.transform.position.y - p.gameObject.transform.lossyScale.y * blockHeight;
				
				if (playersBottom < cameraBottom)
					p.Squish();
			}
		
	}
	
}
