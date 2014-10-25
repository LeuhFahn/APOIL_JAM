using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public enum EPlayerNum
	{
		ePlayer1,
		ePlayer2,
	}

	public EPlayerNum e_playNum;
	SPlayerInput m_playerInput;
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame; 
	void Update () 
	{
		setPlayerInput();
		float f_Velocity = 2.0f;
		Vector3 v3_move = Vector3.zero;
		if(m_playerInput.moveRight)
			v3_move += new Vector3(1, 0, 0);
		if(m_playerInput.moveLeft)
			v3_move += new Vector3(-1, 0, 0);
		if(m_playerInput.moveUp)
			v3_move += new Vector3(0, 1, 0);
		if(m_playerInput.moveDown)
			v3_move += new Vector3(0, -1, 0);

		//v3_move += f_Velocity * new Vector3(m_playerInput.DirectionHorizontal, m_playerInput.DirectionVertical, 0);
	
		gameObject.transform.position += f_Velocity *  v3_move;
		//rigidbody2D.AddForce(v3_move);
	}

	void setPlayerInput()
	{
		if(e_playNum == EPlayerNum.ePlayer1)
			m_playerInput = CApoilInput.InputPlayer1;
		else if(e_playNum == EPlayerNum.ePlayer2)
			m_playerInput = CApoilInput.InputPlayer2;
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		Debug.Log(other.tag);
	}
}
