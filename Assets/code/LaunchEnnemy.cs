using UnityEngine;
using System.Collections;

public class LaunchEnnemy : MonoBehaviour {

	public Ennemy.ETypeLaunchElement m_eTypeEnnemyToLaunch;
	public float f_timerLaunch = 2.0f;
	private float f_timer;
	private GameObject go_ennemy;
	// Use this for initialization
	void Start () 
	{
		f_timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(f_timer > f_timerLaunch)
		{
			launchAnEnnemy();
			f_timer = 0.0f;
		}
		else
		{
			f_timer += Time.deltaTime;
		}
	}

	void launchAnEnnemy()
	{
		GameObject pf_ennemy = null;
		switch(m_eTypeEnnemyToLaunch)
		{
			case Ennemy.ETypeLaunchElement.eCoeur:
			{
				pf_ennemy = GlobalVariable.PF_COEUR;
				break;
			}
			case Ennemy.ETypeLaunchElement.eFleur:
			{
				pf_ennemy = GlobalVariable.PF_FLEUR;
				break;
			}
			case Ennemy.ETypeLaunchElement.eNounours:
			{
				pf_ennemy = GlobalVariable.PF_NOUNOURS;
				break;
			}
			case Ennemy.ETypeLaunchElement.eRayon:
			{
				pf_ennemy = GlobalVariable.PF_RAYON;
				break;
			}
		}

		if(pf_ennemy != null)
		{
			go_ennemy = Object.Instantiate(pf_ennemy) as GameObject;
			go_ennemy.transform.position = this.transform.position;
			go_ennemy.rigidbody2D.velocity = 10.0f * this.rigidbody2D.transform.up;
		}
	}
}
