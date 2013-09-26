using UnityEngine;
using System.Collections;

public class SawSpawner : MonoBehaviour {

	public Transform saw;
	
	public int saws = 10;
	public int distance = 30;
	public int minSize = 1;
	public int maxSize = 8;
	
	public int leftX = 0;
	public int rightX = 0;
	
	bool shoot = false;
	
	// Use this for initialization
	void Start () {
		
		
		for(int i = 1; i < saws+1; i++){
			float size = (float)Random.Range(minSize, maxSize);
			Transform instance;
			if (i%2 == 1)
				instance = (Transform) Instantiate(saw, new Vector3(leftX - size/16, i * distance + 0.5f, 0), Quaternion.identity);
			else
				instance = (Transform) Instantiate(saw, new Vector3(rightX + size/16, i * distance + 0.5f, 0), Quaternion.identity);
			
			instance.transform.localScale = new Vector3(size, 0.0001f, size);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}