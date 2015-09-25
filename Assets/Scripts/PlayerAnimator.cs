using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;


public class PlayerAnimator : MonoBehaviour {

	private Animator anim;
	private bool running;

	private void Start () 
	{
		anim = GetComponent<Animator> ();
	}
	
	private void Update () 
	{
		if (CrossPlatformInputManager.GetAxisRaw("Vertical") > 0) 
		{
			running = true;
		} 
		else 
		{
			running = false;
		}

		anim.SetBool ("Running", running);	
	}
}
