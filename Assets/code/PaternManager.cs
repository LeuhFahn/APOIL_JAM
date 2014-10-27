using UnityEngine;
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
		ePaternLaVague,
		ePaternDazzEstUnCon,
		eShotGun,
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
		if(f_timer < f_Duration)
		{
			f_timer += Time.deltaTime;
		}
		else
		{
			GenerateNewLauncherMap(TypePatern, f_Duration);
			f_timer = 0.0f;
			f_Duration = Random.Range(5.0f,10.0f);
			TypePatern = (Random.Range(0.0f, 100.0f) > 25) ?  ETypePatern.eShotGun :  ETypePatern.ePaternSchredder;
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
			case ETypePatern.ePaternLaVague:
			{
				//GenerateLauncherMapLaVague(_list_launcher, _f_duration);
				break;
			}
			case ETypePatern.ePaternDazzEstUnCon:
			{
				//GenerateLauncherMapDazzEstUnCon(_list_launcher, _f_duration);
				break;
			}
			case ETypePatern.eShotGun:
			{
				//GenerateLauncherMapDazzEstUnCon(_list_launcher, _f_duration);
				CoroutineManager.Instance.StartCoroutine(CoroutineMapShotGun(_f_duration));
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

			
			Vector3 v3_forward = new Vector3(1,0,0);
			Vector3 v3_right = new Vector3(0,1,0);

			float fAngle = Vector3.Angle(v3_forward, Game.tab_player[i].transform.position - new Vector3(fPosX, fPosY, 1));
			if(Vector3.Dot(v3_right,  Game.tab_player[i].transform.position - new Vector3(fPosX, fPosY, 1)) < 0)
				fAngle *= -1;

			fAngle -= 2.0f * f_DeltaAngle*Mathf.Rad2Deg;
			float fAngleDeltaBuff = - 2.0f * f_DeltaAngle*Mathf.Rad2Deg
			for(int j = 0 ; j < n_nbCanon ; ++j)
			{
				GameObject go_launcher = Object.Instantiate(GlobalVariable.PF_LAUNCHER_COEUR) as GameObject;
				
				setVariablesLauncher(go_launcher, 0.0f, fPosX, fPosY , fAngle, f_timerLaunch, f_velocityShot, _f_duration);
			/*	
			//	test
				go_launcher.transform.LookAt(Game.tab_player[i].transform);
				go_launcher.rotate(fAngleDeltaBuff)
				fAngleDeltaBuff += f_DeltaAngle*Mathf.Rad2Deg;
			*/	
				fAngle += f_DeltaAngle*Mathf.Rad2Deg;
				_list_launcher.Add(go_launcher.GetComponent<LaunchEnnemy>());
			}
		}
	}
	
	void GenerateLauncherMapLaVague(List<LaunchEnnemy> _list_launcher, float _f_duration)
	{
	}

	void GenerateLauncherMapDazzEstUnCon(List<LaunchEnnemy> _list_launcher, float _f_duration)
	{
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

	IEnumerator CoroutineMapPaternLaVague(float _f_duration, bool _b_diagonale_HautDroite_BasGauche)
	{
		List<LaunchEnnemy> list_launcher = new List<LaunchEnnemy>();
		float f_time = 0.0f;


		GenerateLauncherMapLaVague(list_launcher, _f_duration);
		
		while(f_time < _f_duration)
		{
			f_time += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		
		list_launcher.Clear();
	}

	IEnumerator CoroutineMapDazzEstUnCon(float _f_duration, bool _b_diagonale_HautDroite_BasGauche)
	{
		List<LaunchEnnemy> list_launcher = new List<LaunchEnnemy>();
		float f_time = 0.0f;
		
		GenerateLauncherMapDazzEstUnCon(list_launcher, _f_duration);
		
		while(f_time < _f_duration)
		{
			f_time += Time.deltaTime;
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

		float[] f_lastAngle = new float[2];
		float[] f_newAngle = new float[2];
		for(int i = 0 ; i < 2 ; ++i)
		{
			f_lastAngle[i] = list_launcher[i*n_nbCanon].variablesDeTrajectoire[3];
			f_newAngle[i] = f_lastAngle[i];
		}

		while(f_time < _f_duration)
		{
			/*Vector3 v3_forward = new Vector3(1,0,0);
			Vector3 v3_right = new Vector3(0,1,0);

			for (int i = 0 ; i < 2 ; ++i)
			{
				f_lastAngle[i] = f_newAngle[i];

				Vector3 v3_Direction = Game.tab_player[i].transform.position - list_launcher[i * n_nbCanon].transform.position;

				f_newAngle[i] = Vector3.Angle(v3_forward, Game.tab_player[i].transform.position - list_launcher[i * n_nbCanon].transform.position);
				if(Vector3.Dot(v3_right,  Game.tab_player[i].transform.position - list_launcher[i * n_nbCanon].transform.position) < 0)
					f_newAngle[i] *= -1;
				 

				f_newAngle[i] -= 2.0f * f_DeltaAngle*Mathf.Rad2Deg;
				
				for(int j = 0 ; j < n_nbCanon ; ++j)
				{
					f_newAngle[i] += f_DeltaAngle*Mathf.Rad2Deg;

					list_launcher[i*n_nbCanon + j].transform.RotateAround(transform.forward , (f_newAngle[i] - f_lastAngle[i]) * Mathf.Deg2Rad);
				}

			}*/


			f_time += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		
		list_launcher.Clear();
	}
}
