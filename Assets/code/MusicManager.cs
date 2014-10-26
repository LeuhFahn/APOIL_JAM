using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip[] randomPhrases = new AudioClip[7] ;
	public AudioSource sourceRandom;
	public AudioSource[] fixPhrases = new AudioSource[4] ;
	// Use this for initialization
	void Awake () 
	{
		sourceRandom.clip = randomPhrases[0];
		sourceRandom.Play();
		foreach(AudioSource audio in fixPhrases)
		{
			audio.Play();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!sourceRandom.isPlaying)
		{
			int id = Random.Range(0,6);
			sourceRandom.clip = randomPhrases[id];
			sourceRandom.Play();
		}
	}

	void SetVolumeMusic(float _f_volume)
	{
		sourceRandom.volume = _f_volume;
		foreach(AudioSource audio in fixPhrases)
		{
			audio.volume = _f_volume;
		}
	}
}
