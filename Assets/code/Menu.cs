using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
	public GameObject UI;
	public GameObject menu_endGame;
	public GameObject menu_start;
	public GameObject menu_credit;
	public GameObject menu_instruc;
	public GameObject menu_perso1_win;
	public GameObject menu_perso2_win;
	public GameObject menu_persoInGame;
	public GameObject menu_perso1_Score;
	public GameObject menu_perso2_Score;
	public GameObject text_credit;

	private static bool b_instantiated = false; // set true à l'init et remis à false en lançant le jeu
	private static Menu _instance = null;
	public static Menu Instance
	{
		get
		{
			if(_instance == null)
			{
				if(!b_instantiated) // pour empecher des instances de se creer apres la fin de la partie
				{
					Init (GameObject.FindObjectOfType(typeof(Menu)) as Menu);
				}
			}

			return _instance;
		}
	}

	static void Init(Menu mgr)
	{
		if(mgr == null)
		{
			GameObject go = Object.Instantiate(GlobalVariable.PF_MENU) as GameObject;
		}

		b_instantiated = true;
		DontDestroyOnLoad(mgr.gameObject);
		_instance = mgr;
		
		mgr.menu_endGame.SetActive(false);
		mgr.menu_start.SetActive(true);
		mgr.menu_credit.SetActive(false);
		mgr.menu_instruc.SetActive(false);
		mgr.menu_persoInGame.SetActive(false);
	}

	void Awake()
	{
		if(_instance == null)
		{
			Init (this);
		}
		else if( _instance != this)
		{
			DestroyImmediate(this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () 
	{
	
	}

	public void commencer()
	{
		Time.timeScale = 1.0f;
		Application.LoadLevel(1);
		menu_endGame.SetActive(false);
		menu_start.SetActive(false);
		menu_credit.SetActive(false);
		menu_instruc.SetActive(false);
		menu_persoInGame.SetActive(true);
	}

	public void instruction()
	{
		menu_endGame.SetActive(false);
		menu_start.SetActive(false);
		menu_credit.SetActive(false);
		menu_instruc.SetActive(true);
		menu_persoInGame.SetActive(false);
	}

	public void credit()
	{
		menu_endGame.SetActive(false);
		menu_start.SetActive(false);
		menu_credit.SetActive(true);
		menu_instruc.SetActive(false);
		menu_persoInGame.SetActive(false);
		text_credit.GetComponent<AnimMenuCredit>().ResetPosText();
	}

	public void quitter()
	{
		Application.Quit();
	}

	public void goMenu()
	{
		Time.timeScale = 1.0f;
		Application.LoadLevel(0);
		menu_endGame.SetActive(false);
		menu_start.SetActive(true);
		menu_credit.SetActive(false);
		menu_instruc.SetActive(false);
		menu_persoInGame.SetActive(false);
	}

	public void rejouer()
	{
		Time.timeScale = 1.0f;
		Application.LoadLevel(1);
		menu_endGame.SetActive(false);
		menu_start.SetActive(false);
		menu_credit.SetActive(false);
		menu_instruc.SetActive(false);
		menu_persoInGame.SetActive(true);
	}

	public void EndGame(Player.EPlayerNum ePlayerNum)
	{
		Time.timeScale = 0.0f;
		menu_endGame.SetActive(true);
		menu_start.SetActive(false);
		menu_credit.SetActive(false);
		menu_instruc.SetActive(false);
		menu_persoInGame.SetActive(true);
		if(ePlayerNum == Player.EPlayerNum.ePlayer1)
		{
			menu_perso1_win.SetActive(false);
			menu_perso2_win.SetActive(true);
		}
		else
		{
			menu_perso1_win.SetActive(true);
			menu_perso2_win.SetActive(false);
		}

	}

	public void SetScorePlayers(int _f_p1, int _f_p2)
	{
		menu_perso1_Score.GetComponent<UILabel>().text = _f_p1.ToString();
		menu_perso2_Score.GetComponent<UILabel>().text = _f_p2.ToString();
	}
}
