using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour {

	private Animator anim;
	private bool running;

	private void Start () 
	{
		anim = GetComponent<Animator> ();
	}
	
	private void Update () 
	{
		if (Input.GetKey (KeyCode.W)) 
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
