using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	int n_NbPlayer = 2;
	GameObject go_camera;
	public static GameObject go_trashContainer;

	public static float f_pointsDeLAmitie;
	public static float f_difficulte;
	public static GameObject[] tab_player = new GameObject[2];

	// Use this for initialization
	void Start () 
	{
		CApoilInput.Init();

		go_camera = Object.Instantiate(GlobalVariable.PF_CAMERA) as GameObject;

		for(int i = 0 ; i < n_NbPlayer ; ++i)
		{
			GameObject go_player = null;// Object.Instantiate(GlobalVariable.PF_PLAYER) as GameObject;
			float f_offset = 30.0f;
			if(i == 0)
			{
				go_player = Object.Instantiate(GlobalVariable.PF_PLAYER_MAN) as GameObject;
				//go_spriteContainer.transform.parent = go_player.transform;
				go_player.GetComponent<Player>().e_playNum = Player.EPlayerNum.ePlayer1;
				tab_player[0] = go_player;
			}
			else
			{
				go_player = Object.Instantiate(GlobalVariable.PF_PLAYER_WOMAN) as GameObject;
				//go_spriteContainer.transform.parent = go_player.transform;
				go_player.GetComponent<Player>().e_playNum = Player.EPlayerNum.ePlayer2;
				f_offset *= -1;
				tab_player[1] = go_player;
			}

			go_player.rigidbody2D.transform.position = new Vector2(Screen.width / 4.0f + (1-i)*Screen.width / 2.0f, Screen.height / 2.0f);
		}

		Game.go_trashContainer = GameObject.Find("_trashContainer");

		Game.f_pointsDeLAmitie = 0.0f;
		Game.f_difficulte = 0.0f;

		PaternManager.Instance.TypePatern = PaternManager.ETypePatern.ePaternSchredder;
		//PaternManager.Instance.GenerateLauncherMap(PaternManager.ETypePatern.ePaternSchredder, 50.0f);
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

	void CalcVariableDifficulteJeanPhe()
	{
		float fDistanceJ1J2 = 1.0f;
		if(GlobalVariable.F_DISTANCE_AMITIE > fDistanceJ1J2)
			Game.f_pointsDeLAmitie += GlobalVariable.F_DISTANCE_AMITIE - fDistanceJ1J2;
		else
			Game.f_pointsDeLAmitie -= fDistanceJ1J2;

		Game.f_pointsDeLAmitie *= Time.deltaTime;
		if(Game.f_pointsDeLAmitie < 0)
			Game.f_pointsDeLAmitie = 0.0f;

		Game.f_difficulte = Game.f_pointsDeLAmitie / Mathf.Max(fDistanceJ1J2, 200.0f);
	}
}
