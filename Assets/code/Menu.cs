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
	public GameObject text_credit;

	// Use this for initialization
	void Awake () 
	{
		menu_endGame.SetActive(false);
		menu_start.SetActive(true);
		menu_credit.SetActive(false);
		menu_instruc.SetActive(false);

		DontDestroyOnLoad(this.gameObject);
		DontDestroyOnLoad(UI);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void commencer()
	{
		Application.LoadLevel(1);
		menu_endGame.SetActive(false);
		menu_start.SetActive(false);
		menu_credit.SetActive(false);
		menu_instruc.SetActive(false);
		UI.SetActive(false);
	}

	public void instruction()
	{
		menu_endGame.SetActive(false);
		menu_start.SetActive(false);
		menu_credit.SetActive(false);
		menu_instruc.SetActive(true);
	}

	public void credit()
	{
		menu_endGame.SetActive(false);
		menu_start.SetActive(false);
		menu_credit.SetActive(true);
		menu_instruc.SetActive(false);
		text_credit.GetComponent<AnimMenuCredit>().ResetPosText();
	}

	public void quitter()
	{
		Application.Quit();
	}

	public void goMenu()
	{
		menu_endGame.SetActive(false);
		menu_start.SetActive(true);
		menu_credit.SetActive(false);
		menu_instruc.SetActive(false);
	}

	public void rejouer()
	{
		Application.LoadLevel(1);
		menu_endGame.SetActive(false);
		menu_start.SetActive(false);
		menu_credit.SetActive(false);
		menu_instruc.SetActive(false);
		UI.SetActive(false);
	}

	public void EndGame(Player.EPlayerNum ePlayerNum)
	{
		UI.SetActive(true);
		menu_endGame.SetActive(true);
		menu_start.SetActive(false);
		menu_credit.SetActive(false);
		menu_instruc.SetActive(false);
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
}
