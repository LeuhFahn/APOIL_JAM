using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoroutineManager : MonoBehaviour
{
	private static bool b_instantiated = false; // set true à l'init et remis à false en lançant le jeu
	private static CoroutineManager _instance = null;
	public static CoroutineManager Instance
	{
		get
		{
			if(_instance == null)
			{
				if(!b_instantiated) // pour empecher des instances de se creer apres la fin de la partie
				{
					Init (GameObject.FindObjectOfType(typeof(CoroutineManager)) as CoroutineManager);
				}
			}

			return _instance;
		}
	}

	public bool b_forbidNewCoroutine = true;

	static void Init(CoroutineManager mgr)
	{
		if(mgr == null)
		{
			GameObject go = new GameObject("PF_CoroutineManager");
			mgr = go.AddComponent<CoroutineManager>();
		}

		b_instantiated = true;
		mgr.b_forbidNewCoroutine = false;
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

	public new UnityEngine.Coroutine StartCoroutine(IEnumerator _coroutine)
	{
		if (!b_forbidNewCoroutine)
		{
			return base.StartCoroutine(_coroutine);
		}
		else
		{
			return null;
		}
	}

	public void forbidCoroutine(bool _b_forbidCoroutine)
	{
		b_forbidNewCoroutine = _b_forbidCoroutine;
	}

	private void OnLevelWasLoaded(int level)
	{
		b_forbidNewCoroutine = false;
	}
}
