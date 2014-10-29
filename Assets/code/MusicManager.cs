using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip[] randomPhrases = new AudioClip[7] ;
	public AudioSource sourceRandom;
	public AudioSource[] fixPhrases = new AudioSource[4] ;

	public AnimationCurve[] DazzCurves = new AnimationCurve[5] ;

	float f_MaxEloignement;

	// Use this for initialization
	void Awake () 
	{
		sourceRandom.clip = randomPhrases[0];
		sourceRandom.Play();
		foreach(AudioSource audio in fixPhrases)
		{
			audio.Play();
		}

		f_MaxEloignement = Mathf.Sqrt(Screen.width * Screen.width + Screen.height * Screen.height);
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

		float f_distance = Vector2.Distance(new Vector2(Game.tab_player[0].transform.position.x, Game.tab_player[0].transform.position.y), 
		                                    new Vector2(Game.tab_player[1].transform.position.x, Game.tab_player[1].transform.position.y));

		float f_ratio = f_distance / f_MaxEloignement;
		Debug.Log(1.0f - f_ratio);
		SetRatioVolumeMusic(1.0f - f_ratio);
	}

	void SetRatioVolumeMusic(float _f_ratio)
	{
		sourceRandom.volume = DazzCurves[0].Evaluate(_f_ratio);
		int id = 1;
		foreach(AudioSource audio in fixPhrases)
		{
			audio.volume = DazzCurves[id].Evaluate(_f_ratio);
			++id;
		}
	}
}
