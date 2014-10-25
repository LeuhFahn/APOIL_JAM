using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	int n_NbPlayer = 2;

	// Use this for initialization
	void Start () 
	{
		CApoilInput.Init();

		for(int i = 0 ; i < n_NbPlayer ; ++i)
		{
			GameObject go_player = Object.Instantiate(GlobalVariable.PF_PLAYER) as GameObject;
			if(i == 0)
				go_player.GetComponent<Player>().e_playNum = Player.EPlayerNum.ePlayer1;
			else
				go_player.GetComponent<Player>().e_playNum = Player.EPlayerNum.ePlayer2;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		CApoilInput.Process(Time.deltaTime);

		if(CApoilInput.Quit)
		{
			Application.Quit();
		}
	}
}
