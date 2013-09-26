using UnityEngine;
using System.Collections;

public class Saw : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.eulerAngles = new Vector3(90f,0f,0f);
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Rotate(new Vector3(0f,-3f,0f));
		
	}
	
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
