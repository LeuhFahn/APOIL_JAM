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
					go_petale.transform.localPosition += go_petale.transform.up * 0.2f;
					go_petale.rigidbody2D.velocity = this.rigidbody2D.velocity;
					//go_petale.rigidbody2D.transform.Translate(5 * go_petale.rigidbody2D.transform.right, Space.Self);
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
				CoroutineManager.Instance.StartCoroutine(launchCoroutineNuage());
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
				GameObject go_cible = null; 
				float f_distancePlayer1 = Vector3.Distance(Game.tab_player[0].transform.position, this.transform.position);
				float f_distancePlayer2 = Vector3.Distance(Game.tab_player[1].transform.position, this.transform.position);
				if(f_distancePlayer1 < f_distancePlayer2)
				{
					go_cible = Game.tab_player[0];
				}
				else
				{
					go_cible = Game.tab_player[1];
				}


				
					
				Vector3 v3_forward = /*new Vector3(1,0,0); */this.rigidbody2D.transform.up;
				Vector3 v3_right = /*new Vector3(0,1,0); */this.rigidbody2D.transform.right;

				float f_angle = Vector3.Angle(v3_right, go_cible.transform.position - this.transform.position);

				f_angle *= Mathf.Deg2Rad;
				if(Vector3.Dot(v3_forward,  go_cible.transform.position - this.transform.position) < 0)
					f_angle *= -1;
				
				f_angle = Mathf.Min(f_angle , Mathf.Sign(f_angle) * GlobalVariable.F_NOUNOURS_ANGLE_PER_SEC * Time.deltaTime);
				Vector3 v3_newDirection = Mathf.Sin (f_angle) * v3_forward + Mathf.Cos(f_angle) * v3_right;
				v3_newDirection *= GlobalVariable.F_NOUNOURS_VELOCITY;
				
				this.rigidbody2D.transform.RotateAround(transform.forward, f_angle + 3.14159f/2.0f);
					//rigidbody2D.transform.up = this.rigidbody2D.transform.position + v3_newDirection;//go_cible.transform.position - this.transform.position ;//AddForce(500.0f * (go_cible.transform.position - this.transform.position).normalized);
				rigidbody2D.velocity = v3_newDirection;
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
		bool b_explosion = false;
		while(!b_explosion)
		{
			float f_distancePlayer1 = Vector3.Distance(Game.tab_player[0].transform.position, this.transform.position);
			float f_distancePlayer2 = Vector3.Distance(Game.tab_player[1].transform.position, this.transform.position);
			if(f_distancePlayer1 <= GlobalVariable.F_FLEUR_DISTANCE_EXPLOSION || f_distancePlayer2 <= GlobalVariable.F_FLEUR_DISTANCE_EXPLOSION)
			{
				transformFleurToPetal();
				b_explosion = true;
			}

			foreach (Transform child in transform) 
			{
				if(child.gameObject.name == "PF_ennemy_petal")
				{
					//child.localPosition = Vector3.zero;
					//child.localPosition += child.rigidbody2D.transform.right * 0.2f;

				}
			}

			yield return new WaitForEndOfFrame();
		}
	}

	IEnumerator launchCoroutineNuage()
	{
		bool b_explosion = false;
		while(!b_explosion)
		{
			float f_distancePlayer1 = Vector3.Distance(Game.tab_player[0].transform.position, this.transform.position);
			float f_distancePlayer2 = Vector3.Distance(Game.tab_player[1].transform.position, this.transform.position);
			if(f_distancePlayer1 <= GlobalVariable.F_NUAGE_DISTANCE_EXPLOSION || f_distancePlayer2 <= GlobalVariable.F_NUAGE_DISTANCE_EXPLOSION)
			{
				transformNuageToSoleil();
				b_explosion = true;
			}
			yield return new WaitForEndOfFrame();
		}
	}

	void transformRayonToArcEnCiel()
	{
		gameObject.transform.FindChild("bad").gameObject.SetActive(false);
		gameObject.transform.FindChild("arc").gameObject.SetActive(true);
	}

	void transformFleurToPetal()
	{
		Transform petale = this.transform.FindChild("PF_ennemy_petal");
		while(petale != null)
		{
			petale.rigidbody2D.velocity = 100.0f * petale.rigidbody2D.transform.right;
			petale.parent = Game.go_trashContainer.transform;
			petale = this.transform.FindChild("PF_ennemy_petal");
		}

		Destroy(this.gameObject);
	}

	void transformNuageToSoleil()
	{
		GameObject go_sun = Object.Instantiate(GlobalVariable.PF_SOLEIL) as GameObject;
		go_sun.transform.position = this.transform.position;
		Destroy(this.gameObject);
	}
}
