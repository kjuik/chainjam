using UnityEngine;
using System.Collections;

public class BlockSpawner : MonoBehaviour {
	
	public Transform platform;
	public Transform mine;
	
	public int blocks = 100;
	public int distance = 3;
	public int maxSize = 4;
	public int minSize = 1;
	public int minX = -23;
	public int maxX = 23;
	public int height = 1;
	
	// Use this for initialization
	void Start () {
		
		for(int i = 0; i < blocks; i++){
			
			float spread = maxSize - minSize;
			float progress = (float)i / (float)blocks;
			
			float width = maxSize - (int)(spread * progress);
			
			Debug.Log("spread: " + spread + ", progress: " + progress + " , width: " + width);
			
			SpawnBlock(i * distance + 0.5f, width + 0.0f,i%2==0);
			
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void SpawnBlock(float y, float width, bool mineActual){
	//Jannick tries to make two platforms the funky way
		float x = Random.Range(minX, maxX);
		float x2 = Random.Range(minX, maxX);
		
		if(mineActual){
			Transform mines = (Transform) Instantiate(mine, new Vector3(x, y+1, 0), Quaternion.identity);
		}
		
		Transform instance = (Transform) Instantiate(platform, new Vector3(x, y, 0), Quaternion.identity);
		
		Transform instance2 = (Transform) Instantiate(platform, new Vector3(x2, y, 0), Quaternion.identity);
		
		Vector3 scale = instance.transform.localScale;
		Vector3 scale2 = instance2.transform.localScale;
		
		scale.x = width;
		scale2.x = width;
		
		instance.transform.localScale = scale;
		instance2.transform.localScale = scale2;
		
		
	}
	
	
}
