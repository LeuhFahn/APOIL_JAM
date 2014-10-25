using UnityEngine;
using System.Collections;

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
	}

	private ETypePatern m_ePatern;
	public ETypePatern TypePatern
	{
		get{return m_ePatern;}
		set{m_ePatern = value;}
	}

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

	void Update()
	{

	}

	public void GenerateLauncherMap(ETypePatern _eTypePatern, float _f_duration)
	{
		switch(_eTypePatern)
		{
			case ETypePatern.ePaternSchredder:
			{
				GenerateLauncherMapPaternSchredder(_f_duration, true);
				break;
			}
			case ETypePatern.ePaternLaVague:
			{
				GenerateLauncherMapLaVague(_f_duration);
				break;
			}
			case ETypePatern.ePaternDazzEstUnCon:
			{
				GenerateLauncherMapDazzEstUnCon(_f_duration);
				break;
			}
		}
	}

	void GenerateLauncherMapPaternSchredder(float _f_duration, bool _b_diagonale_HautDroite_BasGauche)
	{
		float fSizeCase = 2* 30.0f;
		bool b_diagonale_HautDroite_BasGauche = _b_diagonale_HautDroite_BasGauche;
		int nNbLauncherHorizontal = 8;
		int nNbLauncherVerticall = 5;

		float f_velocityShot = 50.0f;
		float f_timerLaunch =  2*2*fSizeCase/f_velocityShot;

		float f_durationPatern = _f_duration;
		float f_decallageDiagonale = b_diagonale_HautDroite_BasGauche ? 0 : 1;

		// haut
		for(int i = 0 ; i < nNbLauncherHorizontal ; ++i)
		{
			GameObject go_launcher = Object.Instantiate(GlobalVariable.PF_LAUNCHER_COEUR) as GameObject;
			setVariablesLauncher(go_launcher, 0.0f, fSizeCase/2.0f + i*2*fSizeCase + f_decallageDiagonale * fSizeCase , Screen.height, 270.0f, f_timerLaunch, f_velocityShot, f_durationPatern);
		}

		// bas
		for(int i = 0 ; i < nNbLauncherHorizontal ; ++i)
		{
			GameObject go_launcher = Object.Instantiate(GlobalVariable.PF_LAUNCHER_COEUR) as GameObject;
			setVariablesLauncher(go_launcher, 0.0f, 3*fSizeCase/2.0f + i*2*fSizeCase - f_decallageDiagonale * fSizeCase, 0.0f, 90.0f, f_timerLaunch, f_velocityShot, f_durationPatern);
		}

		// gauche
		for(int i = 0 ; i < nNbLauncherVerticall ; ++i)
		{
			GameObject go_launcher = Object.Instantiate(GlobalVariable.PF_LAUNCHER_COEUR) as GameObject;
			setVariablesLauncher(go_launcher, 0.0f,0.0f, 3.0f * fSizeCase/2.0f + i*2*fSizeCase - f_decallageDiagonale * fSizeCase , 0.0f, f_timerLaunch, f_velocityShot, f_durationPatern);
		}

		// droite
		for(int i = 0 ; i < nNbLauncherVerticall ; ++i)
		{
			GameObject go_launcher = Object.Instantiate(GlobalVariable.PF_LAUNCHER_COEUR) as GameObject;
			setVariablesLauncher(go_launcher, 0.0f, Screen.width, fSizeCase/2.0f + i*2*fSizeCase + f_decallageDiagonale * fSizeCase , 180.0f, f_timerLaunch, f_velocityShot, f_durationPatern);
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

	void GenerateLauncherMapLaVague(float _f_duration)
	{
	}

	void GenerateLauncherMapDazzEstUnCon(float _f_duration)
	{
	}
}
