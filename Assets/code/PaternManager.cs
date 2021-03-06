﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PaternManager : MonoBehaviour {

	private static bool b_instantiated = false; // set true à l'init et remis à false en lançant le jeu
	private static PaternManager _instance = null;
	public static PaternManager Instance
	{
		get
		{
			if(_instance == null)
			{
				if(!b_instantiated) // pour empecher des instances de se creer apres la fin de la partie
				{
					Init (GameObject.FindObjectOfType(typeof(PaternManager)) as PaternManager);
				}
			}
			
			return _instance;
		}
	}
	
	public enum ETypePatern
	{
		ePaternSchredder,
		//ePaternLaVague,
		//ePaternDazzEstUnCon,
		eShotGun,
		eJenovasDream
	}

	private ETypePatern m_ePatern;
	public ETypePatern TypePatern
	{
		get{return m_ePatern;}
		set{m_ePatern = value;}
	}

	int n_nbPaternLaunched = 1;

	static void Init(PaternManager mgr)
	{
		if(mgr == null)
		{
			GameObject go = new GameObject("PF_PaternManager");
			mgr = go.AddComponent<PaternManager>();
		}
		
		b_instantiated = true;
		DontDestroyOnLoad(mgr.gameObject);
		_instance = mgr;
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

	float f_timer = 0.0f;
	float f_Duration = 2.0f;
	void Update()
	{
		UpdatePatern();
	}

	void UpdatePatern()
	{
		if(f_timer < f_Duration)
		{
			f_timer += Time.deltaTime;
		}
		else
		{
			GenerateNewLauncherMap(TypePatern, f_Duration);
			f_timer = 0.0f;

			float fRand = Random.Range(0.0f, 100.0f);

			if(	fRand < 25.0f)
			{
				f_Duration = Random.Range(5.0f,10.0f);
				TypePatern = ETypePatern.ePaternSchredder;
			}
			else if (fRand < 50.0f)
			{
				f_Duration = Random.Range(2.0f,7.0f);
				TypePatern = ETypePatern.eJenovasDream;
			}
			else
			{
				f_Duration = Random.Range(5.0f,10.0f);
				TypePatern = ETypePatern.eShotGun;
			}
		}
	}


	public void GenerateNewLauncherMap(ETypePatern _eTypePatern, float _f_duration)
	{
		switch(_eTypePatern)
		{
			case ETypePatern.ePaternSchredder:
			{
				CoroutineManager.Instance.StartCoroutine(CoroutineMapPaternSchredder(_f_duration, true));
			    break;
			}
			/*case ETypePatern.ePaternLaVague:
			{
				//GenerateLauncherMapLaVague(_list_launcher, _f_duration);
				break;
			}
			case ETypePatern.ePaternDazzEstUnCon:
			{
				//GenerateLauncherMapDazzEstUnCon(_list_launcher, _f_duration);
				break;
			}*/
			case ETypePatern.eShotGun:
			{
				//GenerateLauncherMapDazzEstUnCon(_list_launcher, _f_duration);
				CoroutineManager.Instance.StartCoroutine(CoroutineMapShotGun(_f_duration));
				break;
			}
			case ETypePatern.eJenovasDream:
			{
				CoroutineManager.Instance.StartCoroutine(CoroutineMapJenovasDream(_f_duration));
				break;
			}

		}
	}

	void GenerateLauncherMapPaternSchredder(List<LaunchEnnemy> _list_launcher, float _f_duration, bool _b_diagonale_HautDroite_BasGauche, float _f_sizeCase)
	{
		float fSizeCase = _f_sizeCase;
		bool b_diagonale_HautDroite_BasGauche = _b_diagonale_HautDroite_BasGauche;
		int nNbLauncherHorizontal = 16;
		int nNbLauncherVerticall = 10;


		float f_velocityShot = 0.25f + Mathf.Sqrt(Game.f_difficulte / n_nbPaternLaunched)/(21.21f /* 4*racine(50) */ );
		f_velocityShot *= GlobalVariable.F_PLAYER_VELOCITY;
		
		f_velocityShot = Mathf.Min (f_velocityShot, GlobalVariable.F_PLAYER_VELOCITY /2.0f);
		
		float f_timerLaunch =  2*2*fSizeCase/f_velocityShot;

		float f_durationPatern = _f_duration;
		float f_decallageDiagonale = b_diagonale_HautDroite_BasGauche ? 0 : 1;

		// haut
		for(int i = 0 ; i < nNbLauncherHorizontal ; ++i)
		{
			GameObject go_launcher = Object.Instantiate(GlobalVariable.PF_LAUNCHER_COEUR) as GameObject;
			setVariablesLauncher(go_launcher, 0.0f, fSizeCase/2.0f + i*2*fSizeCase + f_decallageDiagonale * fSizeCase , Screen.height, 270.0f, f_timerLaunch, f_velocityShot, f_durationPatern);
			_list_launcher.Add(go_launcher.GetComponent<LaunchEnnemy>());
		}

		// bas
		for(int i = 0 ; i < nNbLauncherHorizontal ; ++i)
		{
			GameObject go_launcher = Object.Instantiate(GlobalVariable.PF_LAUNCHER_COEUR) as GameObject;
			setVariablesLauncher(go_launcher, 0.0f, 3*fSizeCase/2.0f + i*2*fSizeCase - f_decallageDiagonale * fSizeCase, 0.0f, 90.0f, f_timerLaunch, f_velocityShot, f_durationPatern);
			_list_launcher.Add(go_launcher.GetComponent<LaunchEnnemy>());
		}

		// gauche
		for(int i = 0 ; i < nNbLauncherVerticall ; ++i)
		{
			GameObject go_launcher = Object.Instantiate(GlobalVariable.PF_LAUNCHER_COEUR) as GameObject;
			setVariablesLauncher(go_launcher, 0.0f,0.0f, 3.0f * fSizeCase/2.0f + i*2*fSizeCase - f_decallageDiagonale * fSizeCase , 0.0f, f_timerLaunch, f_velocityShot, f_durationPatern);
			_list_launcher.Add(go_launcher.GetComponent<LaunchEnnemy>());
		}

		// droite
		for(int i = 0 ; i < nNbLauncherVerticall ; ++i)
		{
			GameObject go_launcher = Object.Instantiate(GlobalVariable.PF_LAUNCHER_COEUR) as GameObject;
			setVariablesLauncher(go_launcher, 0.0f, Screen.width, fSizeCase/2.0f + i*2*fSizeCase + f_decallageDiagonale * fSizeCase , 180.0f, f_timerLaunch, f_velocityShot, f_durationPatern);
			_list_launcher.Add(go_launcher.GetComponent<LaunchEnnemy>());
		}

		if(Game.f_difficulte > 45.0f)
		{
			GameObject go_launcher = Object.Instantiate(GlobalVariable.PF_LAUNCHER_RAYON) as GameObject;

			int nQuelCote = Random.Range(0,3);
			float fPosX = 0.0f;
			float fPosY = 0.0f;
			float fAngle = Random.Range(0.0f,180.0f);;
			if(nQuelCote == 0)
			{
				fPosX = Random.Range(0.0f, 960.0f);
				fPosY = 0.0f;

			}
			else if(nQuelCote == 1)
			{
				fPosX = Random.Range(0.0f, 960.0f);
				fPosY = Screen.height;
			}
			else if(nQuelCote == 2)
			{
				fPosX = 0.0f;
				fPosY = Random.Range(0.0f, 600.0f);
			}
			else if(nQuelCote == 3)
			{
				fPosX = Screen.width;
				fPosY = Random.Range(0.0f, 600.0f);
			}

			setVariablesLauncher(go_launcher, 0.0f, fPosX, fPosY , fAngle, 1.0f, 0.0f, f_durationPatern);
		}

	}

	void setVariablesLauncher(GameObject _go_launcher, float _f_time, float _f_posX, float _f_posY, float _f_Anlge, float _f_timerShot, float _f_velocityShot, float _f_lifeTime)
	{
		_go_launcher.rigidbody2D.transform.position = new Vector2(_f_posX, _f_posY);
		_go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire = new float[4];
		_go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire[0] = _f_time;
		_go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire[1] = _f_posX;
		_go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire[2] = _f_posY;
		_go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire[3] = _f_Anlge;
		_go_launcher.GetComponent<LaunchEnnemy>().f_timerLaunch = _f_timerShot;
		_go_launcher.GetComponent<LaunchEnnemy>().f_Velocity = _f_velocityShot;
		_go_launcher.GetComponent<LaunchEnnemy>().f_lifeTime = _f_lifeTime;
		_go_launcher.transform.parent = Game.go_trashContainer.transform;
	}

	void GenerateLauncherMapShotGun(List<LaunchEnnemy> _list_launcher, float _f_duration, float _f_sizeCase, int _n_nbCanon)
	{
		float f_DeltaAngle = 2.0f * _f_sizeCase / Screen.height;
		int n_nbCanon = _n_nbCanon;
		
		float f_velocityShot = 0.25f + Mathf.Sqrt(Game.f_difficulte / n_nbPaternLaunched)/(21.21f /* 4*racine(50) */ );
		f_velocityShot *= GlobalVariable.F_PLAYER_VELOCITY;
		
		f_velocityShot = Mathf.Min (f_velocityShot, GlobalVariable.F_PLAYER_VELOCITY /2.0f);
		
		float f_timerLaunch =  2*2*_f_sizeCase/f_velocityShot;
		
		for(int i = 0 ; i < 2 ; ++i)
		{
			int nQuelCote = Random.Range(0,3);
			float fPosX = 0.0f;
			float fPosY = 0.0f;

			if(nQuelCote == 0)
			{
				fPosX = Random.Range(0.0f, 960.0f);
				fPosY = 0.0f;
			}
			else if(nQuelCote == 1)
			{
				fPosX = Random.Range(0.0f, 960.0f);
				fPosY = Screen.height;
			}
			else if(nQuelCote == 2)
			{
				fPosX = 0.0f;
				fPosY = Random.Range(0.0f, 600.0f);
			}
			else if(nQuelCote == 3)
			{
				fPosX = Screen.width;
				fPosY = Random.Range(0.0f, 600.0f);
			}


			for(int j = 0 ; j < n_nbCanon ; ++j)
			{
				GameObject go_launcher = Object.Instantiate(GlobalVariable.PF_LAUNCHER_COEUR) as GameObject;
				setVariablesLauncher(go_launcher, 0.0f, fPosX, fPosY , 0.0f, f_timerLaunch, f_velocityShot, _f_duration);
				_list_launcher.Add(go_launcher.GetComponent<LaunchEnnemy>());
			}
		}
	}

	void GenerateLauncherMapPaternJenovasDream(List<LaunchEnnemy> _list_launcher, float _f_duration)
	{
		int nNbLauncherVetical = 4;
		float fSizeCase = Screen.height / nNbLauncherVetical;


		float f_velocityShot = 0.25f + Mathf.Sqrt(Game.f_difficulte / n_nbPaternLaunched)/(21.21f /* 4*racine(50) */ );
		f_velocityShot *= GlobalVariable.F_PLAYER_VELOCITY;
		
		f_velocityShot = Mathf.Min (f_velocityShot, GlobalVariable.F_PLAYER_VELOCITY /2.0f);
		
		float f_timerLaunch =  _f_duration;
		
		float f_durationPatern = _f_duration;

		// gauche
		for(int i = 0 ; i < nNbLauncherVetical ; ++i)
		{
			GameObject go_launcherLeft = Object.Instantiate(GlobalVariable.PF_LAUNCHER_FLEUR) as GameObject;
			GameObject go_launcherRight = Object.Instantiate(GlobalVariable.PF_LAUNCHER_FLEUR) as GameObject;
			setVariablesLauncher(go_launcherLeft, 0.0f,0.0f,fSizeCase / 2.0f + i*fSizeCase, 0.0f, f_timerLaunch, f_velocityShot, f_durationPatern);
			setVariablesLauncher(go_launcherRight, 0.0f,Screen.width,fSizeCase / 2.0f + i*fSizeCase, 0.0f, f_timerLaunch, -f_velocityShot, f_durationPatern);
			_list_launcher.Add(go_launcherLeft.GetComponent<LaunchEnnemy>());
			_list_launcher.Add(go_launcherRight.GetComponent<LaunchEnnemy>());
		}
	}

	IEnumerator CoroutineMapPaternLaVague(float _f_duration, bool _b_diagonale_HautDroite_BasGauche)
	{
		List<LaunchEnnemy> list_launcher = new List<LaunchEnnemy>();
		float f_time = 0.0f;
		
		
		//GenerateLauncherMapLaVague(list_launcher, _f_duration);
		
		while(f_time < _f_duration)
		{
			f_time += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		
		list_launcher.Clear();
	}

	IEnumerator CoroutineMapPaternSchredder(float _f_duration, bool _b_diagonale_HautDroite_BasGauche)
	{
		List<LaunchEnnemy> list_launcher = new List<LaunchEnnemy>();
		float f_time = 0.0f;
		float f_sizeCase = 30.0f;
		GenerateLauncherMapPaternSchredder(list_launcher, _f_duration, _b_diagonale_HautDroite_BasGauche, f_sizeCase);

		while(f_time < _f_duration)
		{
			float f_newVelocity = 0.25f + Mathf.Sqrt(Game.f_difficulte / n_nbPaternLaunched)/(21.21f /* 4*racine(50) */ );
			f_newVelocity *= GlobalVariable.F_PLAYER_VELOCITY;

			f_newVelocity = Mathf.Min (f_newVelocity, GlobalVariable.F_PLAYER_VELOCITY /2.0f);
			float f_timerLaunch =  2*2*f_sizeCase/f_newVelocity;

			foreach(LaunchEnnemy launcher in list_launcher)
			{
				launcher.f_Velocity = f_newVelocity;
				launcher.f_timerLaunch = f_timerLaunch;
			}

			f_time+=Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		list_launcher.Clear();
	}

	IEnumerator CoroutineMapShotGun(float _f_duration)
	{
		List<LaunchEnnemy> list_launcher = new List<LaunchEnnemy>();
		float f_time = 0.0f;
		float f_sizeCase = 30.0f;

		float f_DeltaAngle = 2.0f * f_sizeCase / Screen.height;

		int n_nbCanon = 1;
		if(Game.f_difficulte > 20.0f)
			n_nbCanon = 3;
		if(Game.f_difficulte > 35.0f)
			n_nbCanon = 5;
		
		GenerateLauncherMapShotGun(list_launcher, _f_duration, f_sizeCase, n_nbCanon);

		while(f_time < _f_duration)
		{
			for (int i = 0 ; i < 2 ; ++i)
			{
				Vector3 v3_Direction = Game.tab_player[i].transform.position - list_launcher[i * n_nbCanon].transform.position;
				int nDecal = (n_nbCanon - 1)/2;
				float f_AngleDecal = -nDecal * f_DeltaAngle*Mathf.Rad2Deg;
				for(int j = 0 ; j < n_nbCanon ; ++j)
				{
					list_launcher[i*n_nbCanon + j].transform.up = v3_Direction;
					list_launcher[i*n_nbCanon + j].transform.RotateAround(-this.transform.forward, f_AngleDecal);
					f_AngleDecal += f_DeltaAngle*Mathf.Rad2Deg;
				}
			}

			f_time += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		
		list_launcher.Clear();
	}

	IEnumerator CoroutineMapJenovasDream(float _f_duration)
	{
		List<LaunchEnnemy> list_launcher = new List<LaunchEnnemy>();
		float f_time = 0.0f;
		
		GenerateLauncherMapPaternJenovasDream(list_launcher, _f_duration);
		
		while(f_time < _f_duration)
		{
			f_time += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		
		list_launcher.Clear();
	}
}
