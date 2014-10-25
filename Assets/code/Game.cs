﻿using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	int n_NbPlayer = 2;
	GameObject go_camera;
	public static GameObject go_trashContainer;
	// Use this for initialization
	void Start () 
	{
		CApoilInput.Init();

		go_camera = Object.Instantiate(GlobalVariable.PF_CAMERA) as GameObject;

		for(int i = 0 ; i < n_NbPlayer ; ++i)
		{
			GameObject go_player = Object.Instantiate(GlobalVariable.PF_PLAYER) as GameObject;
			if(i == 0)
				go_player.GetComponent<Player>().e_playNum = Player.EPlayerNum.ePlayer1;
			else
				go_player.GetComponent<Player>().e_playNum = Player.EPlayerNum.ePlayer2;
		}

		Game.go_trashContainer = GameObject.Find("_trashContainer");

		PaternManager.Instance.TypePatern = PaternManager.ETypePatern.ePaternSchredder;
		PaternManager.Instance.GenerateLauncherMap(PaternManager.ETypePatern.ePaternSchredder);
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

	void InitCamera(GameObject _go_camera)
	{
		_go_camera.transform.position = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, -10.0f);
		_go_camera.GetComponent<Camera>().orthographicSize = Screen.height / 2.0f;
	}
}
