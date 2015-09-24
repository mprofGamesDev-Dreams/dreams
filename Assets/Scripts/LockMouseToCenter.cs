using UnityEngine;
using System.Collections;

/*
*	Script to lock the mouse to the center of the screen
*
*	ATTACHMENT: Add to player prefab.
*
*	VARIABLES: 
*				ToggleKey - Key to use to toggle mouse confinement.
*
 */

public class LockMouseToCenter : MonoBehaviour
{
	public bool LockMouse = false;
	public KeyCode ToggleKey;

	// Use this for initialization
	void Start ()
	{
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( Input.GetKeyDown(ToggleKey))
		{
			LockMouse = !LockMouse;
			if( LockMouse )
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
