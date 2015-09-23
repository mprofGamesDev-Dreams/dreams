using UnityEngine;
using System.Collections;

/*
	Script to lock the mouse to the center of the screen
	Use Left Shift to unlock
 */

public class LockMouseToCenter : MonoBehaviour
{
	public bool lockMouse = false;

	// Use this for initialization
	void Start ()
	{
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( Input.GetKeyDown(KeyCode.RightShift))
		{
			lockMouse = !lockMouse;
			if( lockMouse )
			{
				Cursor.lockState = CursorLockMode.Confined;
				Cursor.visible = false;
			}
			else 
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}
	}
}
