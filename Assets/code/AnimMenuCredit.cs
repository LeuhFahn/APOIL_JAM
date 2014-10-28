using UnityEngine;
using System.Collections;

public class AnimMenuCredit : MonoBehaviour {

	public float f_velocity = 2.0f;
	Vector3 posInit = Vector3.zero;
	bool b_init = false;

	void InitPosText()
	{
		posInit = transform.position;
	}

	public void ResetPosText()
	{
		transform.position = posInit;
	}

	// Use this for initialization
	void Awake () 
	{
		InitPosText();
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(new Vector3(0,f_velocity*Time.deltaTime,0));
	}
}
