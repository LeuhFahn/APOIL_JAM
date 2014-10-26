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

	public GameObject go_head;
	public GameObject go_body;
	// Use this for initialization
	void Start () 
	{
		go_body.GetComponent<Animator>().speed *= 0.5f;
	}
	
	// Update is called once per frame; 
	void Update () 
	{
		setPlayerInput();
		float f_Velocity = GlobalVariable.F_PLAYER_VELOCITY;
		Vector3 v3_move = Vector3.zero;

		bool b_moveLeft = m_playerInput.moveLeft;
		bool b_moveRight = m_playerInput.moveRight;
		bool b_moveUp = m_playerInput.moveUp;
		bool b_moveDown = m_playerInput.moveDown;

		if(b_moveRight)
			v3_move += new Vector3(1, 0, 0);
		if(b_moveLeft)
			v3_move += new Vector3(-1, 0, 0);
		if(b_moveUp)
			v3_move += new Vector3(0, 1, 0);
		if(b_moveDown)
			v3_move += new Vector3(0, -1, 0);

		if( b_moveLeft || b_moveRight || b_moveUp || b_moveDown)
		{
			go_body.GetComponent<Animator>().SetBool("b_walk", true);
		}
		else
		{
			go_body.GetComponent<Animator>().SetBool("b_walk", false);
		}

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
