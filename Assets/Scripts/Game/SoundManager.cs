using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	public static SoundManager i;				// The Soundmanager class is basically a Singleton, it should only ever be created once
												//... but we need it like this so we can have them in the inspector
	public AudioClip Jump;
	public AudioClip Squish;
	public AudioClip Boom;
	public AudioClip Laser;
	public AudioClip Background;
	public AudioClip Respawn;			// Just add more sounds here, and you'll be able to use them anywhere in the code by doing
												// SoundManager.i.Play(SoundManager.i.MySound);	
	void Awake () {
		i=this;
		Play(Background);
	}
	
	public void Play(AudioClip clip) 
	{
		audio.PlayOneShot(clip);
	}
}
