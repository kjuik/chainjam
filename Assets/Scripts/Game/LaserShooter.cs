using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserShooter : MonoBehaviour {
	
	public Transform laserShooter;
	public Transform laserBeam;
	
	public int blocks = 10;
	public int distance = 50;
	
	bool shoot = false;
	
	// Use this for initialization
	void Start () {
		
		for(int i = 0; i < blocks; i++){
			Transform instance = (Transform) Instantiate(laserShooter, new Vector3(0, i * distance + 0.5f, 0), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(shoot==false){
			StartCoroutine(Shooting());
		}
	}
	
	IEnumerator Shooting(){
		shoot = true;
		int r = Random.Range(1,4);
		yield return new WaitForSeconds(r);
		List<GameObject> instances = new List<GameObject>();
		for(int i = 0; i < blocks; i++){
			instances.Add(((Transform) Instantiate(laserBeam, new Vector3(0, i * distance + 0.5f, 0), Quaternion.identity)).gameObject);
		}
		yield return new WaitForSeconds(r);
		
		foreach(GameObject instance in instances)
			GameObject.Destroy(instance);
		
		shoot = false;
	}
}
