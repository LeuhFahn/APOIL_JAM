using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LaunchEnnemy : MonoBehaviour {

	public Ennemy.ETypeLaunchElement m_eTypeEnnemyToLaunch;
	public float f_timerLaunch = 2.0f;
	public float f_Velocity = 10.0f;
	private float f_timer;
	private GameObject go_ennemy;

	public struct TrajectoireFrame
	{
		public float f_time;
		public float f_posX;
		public float f_posY;
		public float f_Angle;
	}
	
	public float[] variablesDeTrajectoire;
	private List<TrajectoireFrame> list_trajectoire;
	private int n_SizeTrajectoire;

	//public 
	// Use this for initialization
	void Start () 
	{
		f_timer = 0.0f;
		n_SizeTrajectoire = variablesDeTrajectoire.Length/4;
		list_trajectoire = new List<TrajectoireFrame>(n_SizeTrajectoire);

		SetTrajectoire();

		CoroutineManager.Instance.StartCoroutine(launchEvents());
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

	void AddTrajectoire(float _f_time, float _f_posX, float _f_posY, float _f_Angle)
	{
		TrajectoireFrame frame = new TrajectoireFrame();
		frame.f_Angle = _f_Angle;
		frame.f_posX = _f_posX;
		frame.f_posY = _f_posY;
		frame.f_time = _f_time;
		list_trajectoire.Add(frame);
	}

	void SetTrajectoire()
	{
		int nBuff = 0;
		for(int i = 0 ; i < n_SizeTrajectoire ; ++i)
		{
			AddTrajectoire(variablesDeTrajectoire[nBuff], 
			               variablesDeTrajectoire[nBuff+1], 
			               variablesDeTrajectoire[nBuff+2], 
			               variablesDeTrajectoire[nBuff+3]);
			nBuff += 4;
		}
	}

	void SetLauncherAtFrame(int nFrame)
	{
		rigidbody2D.transform.position = new Vector2(list_trajectoire[nFrame].f_posX, list_trajectoire[nFrame].f_posY);
		float f_lastAngle = (nFrame > 0) ? list_trajectoire[nFrame - 1].f_Angle : 0.0f;
		transform.RotateAround(transform.forward , (list_trajectoire[nFrame].f_Angle - f_lastAngle) * Mathf.Deg2Rad);
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
			go_ennemy.rigidbody2D.velocity = f_Velocity * this.rigidbody2D.transform.up;
			go_ennemy.transform.parent = Game.go_trashContainer.transform;
		}
	}

	IEnumerator launchEvents()
	{
		float fTime = 0.0f;
		int nNextFrame = 0;

		for(int i = 0 ; i < n_SizeTrajectoire ; ++i)
		{
			while(fTime < list_trajectoire[i].f_time)
			{
				fTime += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
			fTime = 0.0f;
			SetLauncherAtFrame(i);
		}

	}
}
