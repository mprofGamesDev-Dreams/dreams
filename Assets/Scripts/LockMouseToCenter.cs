using UnityEngine;
using System.Collections;

/*
	Script to lock the mouse to the center of the screen
	Use Left Shift to unlock
 */

public class LockMouseToCenter : MonoBehaviour
{
	public bool LockMouse = false;

	// Use this for initialization
	void Start ()
	{
		Screen.lockCursor = LockMouse;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( Input.GetKeyDown(KeyCode.RightShift))
		{
			if( LockMouse )
				LockMouse = false;
			else 
				LockMouse = true;

			Screen.lockCursor = LockMouse;
		}
	}
}
