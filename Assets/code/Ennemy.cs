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
			//for(int i = 0 ; i < 
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

	void transformRayonToArcEnCiel()
	{
		gameObject.transform.FindChild("bad").gameObject.SetActive(false);
		gameObject.transform.FindChild("arc").gameObject.SetActive(true);
	}
}
