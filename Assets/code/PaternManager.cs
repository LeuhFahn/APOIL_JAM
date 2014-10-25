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

	public void GenerateLauncherMap(ETypePatern _eTypePatern)
	{
		switch(_eTypePatern)
		{
			case ETypePatern.ePaternSchredder:
			{
				GenerateLauncherMapPaternSchredder();
				break;
			}
			case ETypePatern.ePaternLaVague:
			{
				GenerateLauncherMapLaVague();
				break;
			}
			case ETypePatern.ePaternDazzEstUnCon:
			{
				GenerateLauncherMapDazzEstUnCon();
				break;
			}
		}
	}

	void GenerateLauncherMapPaternSchredder()
	{
		float fSizeCase = 30.0f;
		bool b_diagonale_HautDroite_BasGauche = true;
		int nNbLauncher = 52;//2*16+2*10;
		int nNbLauncherHorizontal = 16;
		int nNbLauncherVerticall = 10;

		float f_velocityShot = 50.0f;
		float f_timerLaunch =  2*fSizeCase/f_velocityShot;

		// haut
		for(int i = 0 ; i < nNbLauncherHorizontal ; ++i)
		{
			GameObject go_launcher = Object.Instantiate(GlobalVariable.PF_LAUNCHER_COEUR) as GameObject;
			go_launcher.rigidbody2D.transform.position = new Vector2(Screen.width, Screen.height);
			go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire = new float[4];
			go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire[0] = 0.0f;
			go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire[1] = fSizeCase/2.0f + i*2*fSizeCase;
			go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire[2] = Screen.height;
			go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire[3] = 270.0f;
			go_launcher.GetComponent<LaunchEnnemy>().f_timerLaunch = f_timerLaunch;
			go_launcher.GetComponent<LaunchEnnemy>().f_Velocity = f_velocityShot;
			go_launcher.GetComponent<LaunchEnnemy>().f_lifeTime = 50.0f;
		}

		// bas
		for(int i = 0 ; i < nNbLauncherHorizontal ; ++i)
		{
			GameObject go_launcher = Object.Instantiate(GlobalVariable.PF_LAUNCHER_COEUR) as GameObject;
			go_launcher.rigidbody2D.transform.position = new Vector2(Screen.width, Screen.height);
			go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire = new float[4];
			go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire[0] = 0.0f;
			go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire[1] = 3*fSizeCase/2.0f + i*2*fSizeCase;
			go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire[2] = 0.0f;
			go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire[3] = 90.0f;
			go_launcher.GetComponent<LaunchEnnemy>().f_timerLaunch = f_timerLaunch;
			go_launcher.GetComponent<LaunchEnnemy>().f_Velocity = f_velocityShot;
			go_launcher.GetComponent<LaunchEnnemy>().f_lifeTime = 50.0f;
		}

		// gauche
		for(int i = 0 ; i < nNbLauncherVerticall ; ++i)
		{
			GameObject go_launcher = Object.Instantiate(GlobalVariable.PF_LAUNCHER_COEUR) as GameObject;
			go_launcher.rigidbody2D.transform.position = new Vector2(Screen.width, Screen.height);
			go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire = new float[4];
			go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire[0] = 0.0f;
			go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire[1] = 0.0f;
			go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire[2] = fSizeCase/2.0f + i*2*fSizeCase;
			go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire[3] = 0.0f;
			go_launcher.GetComponent<LaunchEnnemy>().f_timerLaunch = f_timerLaunch;
			go_launcher.GetComponent<LaunchEnnemy>().f_Velocity = f_velocityShot;
			go_launcher.GetComponent<LaunchEnnemy>().f_lifeTime = 50.0f;
		}

		// droite
		for(int i = 0 ; i < nNbLauncherVerticall ; ++i)
		{
			GameObject go_launcher = Object.Instantiate(GlobalVariable.PF_LAUNCHER_COEUR) as GameObject;
			go_launcher.rigidbody2D.transform.position = new Vector2(Screen.width, Screen.height);
			go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire = new float[4];
			go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire[0] = 0.0f;
			go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire[1] = Screen.width;
			go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire[2] = 3*fSizeCase/2.0f + i*2*fSizeCase;
			go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire[3] = 180.0f;
			go_launcher.GetComponent<LaunchEnnemy>().f_timerLaunch = f_timerLaunch;
			go_launcher.GetComponent<LaunchEnnemy>().f_Velocity = f_velocityShot;
			go_launcher.GetComponent<LaunchEnnemy>().f_lifeTime = 50.0f;
		}


	}

	void GenerateLauncherMapLaVague()
	{
	}

	void GenerateLauncherMapDazzEstUnCon()
	{
	}
}
