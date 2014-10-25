using UnityEngine;
using System.Collections;

public struct SPlayerInput
{
	public float DirectionHorizontal;
	public float DirectionVertical;

	public bool moveRight;
	public bool moveLeft;
	public bool moveUp;
	public bool moveDown;
}

public class CApoilInput
{
	public static SPlayerInput InputPlayer1;
	public static SPlayerInput InputPlayer2;

	public static bool Quit;

	//Debug
	public static bool DebugF9;
	public static bool DebugF10;
	public static bool DebugF11;
	public static bool DebugF12;

	public static void Init()
	{
	}
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public static void Process(float fDeltatime) 
	{	
		InputPlayer1.DirectionHorizontal = Input.GetAxis("Player1_Horizontal");
		InputPlayer1.DirectionVertical = Input.GetAxis("Player1_Vertical");
		InputPlayer2.DirectionHorizontal = Input.GetAxis("Player2_Horizontal");
		InputPlayer2.DirectionVertical = Input.GetAxis("Player2_Vertical");

		InputPlayer1.moveRight = Input.GetAxis("Player1_Horizontal") > 0;
		InputPlayer1.moveLeft = Input.GetAxis("Player1_Horizontal") < 0;
		InputPlayer1.moveUp = Input.GetAxis("Player1_Vertical") > 0;
		InputPlayer1.moveDown = Input.GetAxis("Player1_Vertical") < 0;

		InputPlayer2.moveRight = Input.GetAxis("Player2_Horizontal") > 0;
		InputPlayer2.moveLeft = Input.GetAxis("Player2_Horizontal") < 0;
		InputPlayer2.moveUp = Input.GetAxis("Player2_Vertical") > 0;
		InputPlayer2.moveDown = Input.GetAxis("Player2_Vertical") < 0;

		Quit = Input.GetKey(KeyCode.Escape);
		
		DebugF9 = Input.GetKeyDown(KeyCode.F9);
		DebugF10 = Input.GetKeyDown(KeyCode.F10);
		DebugF11 = Input.GetKeyDown(KeyCode.F11);
		DebugF12 = Input.GetKeyDown(KeyCode.F12);
	}
	
}
