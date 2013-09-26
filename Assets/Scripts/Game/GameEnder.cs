using UnityEngine;
using System.Collections;

public class GameEnder : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		StartCoroutine(WaitAndEnd());
	}
	
	IEnumerator WaitAndEnd(){
		yield return new WaitForSeconds(55);
		
		SoundManager.i.Play(SoundManager.i.Boom);
		SoundManager.i.Play(SoundManager.i.Boom);
		SoundManager.i.Play(SoundManager.i.Boom);
		SoundManager.i.Play(SoundManager.i.Boom);
		
		Time.timeScale = 0.01f;
		GetComponent<DeathsCounter>().finalize();
		
	}
}
