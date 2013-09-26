using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour {

	void OnTriggerEnter (Collider collider) {
		
		// If the player touches the lava, squish 'em!
		if(collider.rigidbody.tag == "Player")
		{
			collider.rigidbody.GetComponent<Player>().Squish();
			
			if(tag=="Mine"){
				GameObject.Destroy(gameObject);
			}
			
		}
		
		
    }
}
