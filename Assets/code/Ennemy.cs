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
		eLaser,
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
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
