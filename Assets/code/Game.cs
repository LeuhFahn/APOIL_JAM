using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	int n_NbPlayer = 2;
	GameObject go_camera;
	public static GameObject go_trashContainer;

	public static float f_pointsDeLAmitie;
	public static float f_difficulte;
	public static float f_coinCpt;

	public static GameObject[] tab_player = new GameObject[2];
	static GameObject go_menu;
	public static bool b_coinInGame = false;

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
				go_player.GetComponent<Player>().e_playNum = Player.EPlayerNum.ePlayer1;
				tab_player[0] = go_player;
			}
			else
			{
				go_player = Object.Instantiate(GlobalVariable.PF_PLAYER_WOMAN) as GameObject;
				go_player.GetComponent<Player>().e_playNum = Player.EPlayerNum.ePlayer2;
				f_offset *= -1;
				tab_player[1] = go_player;
			}

			go_player.rigidbody2D.transform.position = new Vector2(Screen.width / 4.0f + (1-i)*Screen.width / 2.0f, Screen.height / 2.0f);
		}

		Game.go_trashContainer = GameObject.Find("_trashContainer");

		Game.f_pointsDeLAmitie = 0.0f;
		Game.f_difficulte = 0.0f;

		Game.f_coinCpt = 0.0f;
		Game.b_coinInGame = false;
		Game.go_menu = Menu.Instance.gameObject;

		PaternManager.Instance.TypePatern = PaternManager.ETypePatern.eJenovasDream;
		//PaternManager.Instance.GenerateNewLauncherMap(PaternManager.ETypePatern.eJenovasDream, 5.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		CApoilInput.Process(Time.deltaTime);
		CalcVariableDifficulteJeanPhe();

		Menu.Instance.SetScorePlayers(tab_player[1].GetComponent<Player>().NbCoin, tab_player[0].GetComponent<Player>().NbCoin);

		if(Game.f_coinCpt >= 500.0f && !Game.b_coinInGame)
		{
			GenerateCoin();
			Game.f_coinCpt = 0.0f;
		}

		if(tab_player[1].GetComponent<Player>().NbCoin >= 3 + tab_player[0].GetComponent<Player>().NbCoin)
			Menu.Instance.EndGame (Player.EPlayerNum.ePlayer1);
		if(tab_player[0].GetComponent<Player>().NbCoin >= 3 + tab_player[1].GetComponent<Player>().NbCoin)
			Menu.Instance.EndGame (Player.EPlayerNum.ePlayer2);

		/*if(CApoilInput.Quit)
		{
			Application.Quit();
		}*/
	}

	void OnGUI()
	{
		GUI.Label(new Rect(10,10,500,100), "difficulte : "+((int)Game.f_difficulte).ToString() + "coin cpt : "+((int)Game.f_coinCpt).ToString());
	}

	void InitCamera(GameObject _go_camera)
	{
		_go_camera.transform.position = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, -10.0f);
		_go_camera.GetComponent<Camera>().orthographicSize = Screen.height / 2.0f;
	}

	void CalcVariableDifficulteJeanPhe()
	{
		float fDistanceJ1J2 = Vector3.Distance(tab_player[0].transform.position , tab_player[1].transform.position);
		if(GlobalVariable.F_DISTANCE_AMITIE > fDistanceJ1J2)
		{
			Game.f_pointsDeLAmitie += (GlobalVariable.F_DISTANCE_AMITIE - fDistanceJ1J2)* Time.deltaTime;
			if(!Game.b_coinInGame)
				Game.f_coinCpt += (GlobalVariable.F_DISTANCE_AMITIE - fDistanceJ1J2)* Time.deltaTime;
		}
		else
			Game.f_pointsDeLAmitie -= fDistanceJ1J2 * Time.deltaTime;

		if(Game.f_pointsDeLAmitie < 0)
			Game.f_pointsDeLAmitie = 0.0f;


		Game.f_difficulte = Game.f_pointsDeLAmitie / Mathf.Max(fDistanceJ1J2, 200.0f);
	}

	void GenerateCoin()
	{
		Game.b_coinInGame = true;
		GameObject go_coin = Object.Instantiate(GlobalVariable.PF_COIN) as GameObject;
		Vector3 pos = Vector3.zero;

		int deltaPos = 10;

		pos.x = Random.Range(deltaPos ,Screen.width - deltaPos);
		pos.y = Random.Range(deltaPos ,Screen.height - deltaPos);
		go_coin.transform.position = pos;
	}

	public static void EndGame(Player playerWin)
	{
		Menu.Instance.EndGame(playerWin.e_playNum);
	}

	public static void CoinIsCatch()
	{
		Game.b_coinInGame = false;
	}
	
}
