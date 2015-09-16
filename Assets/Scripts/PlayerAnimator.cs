using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour {

	Animator anim;
	bool attack;
	bool running;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		attack = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (attack == true) 
		{
			attack = false;
		}

		if (Input.GetMouseButtonDown (0)) 
		{
			attack = true;
		}

		if (Input.GetKey (KeyCode.W)) 
		{
			running = true;
		} 
		else 
		{
			running = false;
		}

		anim.SetBool ("Running", running);
		anim.SetBool ("Attack", attack);
	
	}
}
