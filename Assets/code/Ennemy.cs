using UnityEngine;
using System.Collections;

public class Ennemy : MonoBehaviour {

	public enum EtypeEnnemy
	{
		eCoeur,
		eFleur,
		ePetale,
		eNounours,
		eNuage,
		eSoleil,
		eArcEnCiel,
		eRayon
	}

	public enum ETypeLaunchElement
	{
		eCoeur,
		eFleur,
		eNounours,
		eRayon,
		eNuage,
	}

	public bool b_KillPlayer = true;
	public EtypeEnnemy m_eType;

	// Use this for initialization
	void Start () 
	{
		GameObject.Destroy(this.gameObject, 20.0f);
		InitEnnemyType();
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateEnnemyType();
	}

	void InitEnnemyType()
	{
		switch(m_eType)
		{
			case Ennemy.EtypeEnnemy.eCoeur:
			{
				break;
			}
			case Ennemy.EtypeEnnemy.eFleur:
			{
				float f_angleRot = 0.0f;
				float f_deltaAngle = 2.0f * Mathf.PI / (float)GlobalVariable.N_NB_PETALE;
				for(int i = 0 ; i < GlobalVariable.N_NB_PETALE ; ++i)
				{
					GameObject go_petale = Object.Instantiate(GlobalVariable.PF_PETAL) as GameObject;
					go_petale.transform.position = this.transform.position;
					go_petale.name = "PF_ennemy_petal";
					go_petale.transform.RotateAround(transform.forward, f_angleRot);
					go_petale.transform.parent = this.transform;
					f_angleRot += f_deltaAngle;
				}
				CoroutineManager.Instance.StartCoroutine(launchCoroutineFleur());
				break;
			}
			case Ennemy.EtypeEnnemy.eNounours:
			{
				break;
			}
			case Ennemy.EtypeEnnemy.eRayon:
			{
				float f_length = Mathf.Sqrt(Screen.width * Screen.width + Screen.height * Screen.height);
				Vector3 scale = this.transform.localScale;// += new Vector3(f_length, 1, 1);
				scale.x *= f_length;
				this.transform.localScale = scale;
				CoroutineManager.Instance.StartCoroutine(launchCoroutineRayon());
				break;
			}
			case Ennemy.EtypeEnnemy.eNuage:
			{
				break;
			}
			case Ennemy.EtypeEnnemy.ePetale:
			{
				break;
			}
			case Ennemy.EtypeEnnemy.eSoleil:
			{
				break;
			}
			case Ennemy.EtypeEnnemy.eArcEnCiel:
			{
				break;
			}
		}
	}

	void UpdateEnnemyType()
	{
		switch(m_eType)
		{
			case Ennemy.EtypeEnnemy.eCoeur:
			{
				break;
			}
			case Ennemy.EtypeEnnemy.eFleur:
			{
				break;
			}
			case Ennemy.EtypeEnnemy.eNounours:
			{
				break;
			}
			case Ennemy.EtypeEnnemy.eRayon:
			{
				break;
			}
			case Ennemy.EtypeEnnemy.eNuage:
			{
				break;
			}
			case Ennemy.EtypeEnnemy.ePetale:
			{
				
				break;
			}
			case Ennemy.EtypeEnnemy.eSoleil:
			{
				break;
			}
			case Ennemy.EtypeEnnemy.eArcEnCiel:
			{
				break;
			}
		}
	}

	IEnumerator launchCoroutineRayon()
	{
		float f_timer = 0.0f;

		while(f_timer < GlobalVariable.F_TIME_TRANSFORMATION_RAYON_ARC)
		{
			f_timer += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		transformRayonToArcEnCiel();
	}

	IEnumerator launchCoroutineFleur()
	{
		float f_timer = 0.0f;
		
		while(f_timer < GlobalVariable.F_TIME_TRANSFORMATION_FLEUR_PETALE)
		{
			foreach (Transform child in transform) 
			{
				if(child.gameObject.name == "PF_ennemy_petal")
					child.localPosition = Vector3.zero;
			}

			f_timer += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		
		transformFleurToPetal();
	}

	void transformRayonToArcEnCiel()
	{
		gameObject.transform.FindChild("bad").gameObject.SetActive(false);
		gameObject.transform.FindChild("arc").gameObject.SetActive(true);
	}

	void transformFleurToPetal()
	{
		foreach (Transform child in transform) 
		{
			if(child.gameObject.name == "PF_ennemy_petal")
				child.parent = Game.go_trashContainer.transform;
		}
		Destroy(this.gameObject);
	}
}
