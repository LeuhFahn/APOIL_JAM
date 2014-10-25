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
		int nNbLauncher = 5;
		GameObject go_launcher = Object.Instantiate(GlobalVariable.PF_LAUNCHER_COEUR) as GameObject;
		go_launcher.rigidbody2D.transform.position = new Vector2(Screen.width, Screen.height);
		go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire = new float[4];
		go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire[0] = 0.0f;
		go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire[1] = Screen.width;
		go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire[2] = Screen.height;
		go_launcher.GetComponent<LaunchEnnemy>().variablesDeTrajectoire[3] = 225.0f;
	}

	void GenerateLauncherMapLaVague()
	{
	}

	void GenerateLauncherMapDazzEstUnCon()
	{
	}
}
