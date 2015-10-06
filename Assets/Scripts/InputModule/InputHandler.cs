using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;

public enum EControlConstraints
{
	EnableAllExceptPowers,
	EnableAll,
	DisableAll,
	DisableAllExceptCamera
}

/// <summary>
/// Handles any input that is not dealt with by the FirstPersonController
/// and then passes an event to the relevant script.
/// Nash - 22/09
/// </summary>
public class InputHandler : MonoBehaviour
{
	bool shoot;
	bool melee;
	bool sprint;
	bool interact;
	bool nextPower;
	bool prevPower;
	bool skip;

	private EControlConstraints controllerConstraints = EControlConstraints.EnableAll; public EControlConstraints ControllerConstraints { get { return controllerConstraints; } set { controllerConstraints = value; }  }

//	AbilityBehaviours abilityBehaviours;
//	FirstPersonController firstPersonController;
//	PunchingControllerPrototype punchingControllerPrototype;

	void Start()
	{
		sprint = false;
//		abilityBehaviours = (AbilityBehaviours)gameObject.GetComponent ("AbilityBehaviours");
//		firstPersonController = (FirstPersonController)gameObject.GetComponent ("FirstPersonController");
//		punchingControllerPrototype = (PunchingControllerPrototype)gameObject.GetComponent ("PunchingControllerPrototype");
	}

	void Update()
	{
		//Make all triggers false
		shoot = false;
		melee = false;
		interact = false;
		nextPower = false;
		prevPower = false;
		skip = false;
		
		if(CrossPlatformInputManager.GetButtonDown ("Pause"))
		{
			if(Time.timeScale == 0)
				Time.timeScale = 1;
			else Time.timeScale = 0 ;

			// show pause menu?
		}



		if(CrossPlatformInputManager.GetButton("Skip"))
		{
			Debug.Log("Clicked Skip Button");
			skip = true;
		}

		switch (controllerConstraints)
		{
			case EControlConstraints.EnableAll:
				//Check for inputs
				//Left trigger
				if((CrossPlatformInputManager.GetAxis ("Melee") > 0 || CrossPlatformInputManager.GetButtonDown ("Melee")) && Time.timeScale==1.0f)
				{
					melee = true;
				}
				//Right trigger
				if(CrossPlatformInputManager.GetAxis ("Shoot") > 0 || CrossPlatformInputManager.GetButtonDown ("Shoot"))
				{
					//abilityBehaviours.shootRay();
					shoot = true;
				}
				//Left bumper
				if(CrossPlatformInputManager.GetButtonDown ("PrevPower"))
				{
					prevPower = true;
				}
				//Right bumper
				if(CrossPlatformInputManager.GetButtonDown ("NextPower"))
				{
					nextPower = true;
				}
				//X button
				if(CrossPlatformInputManager.GetButtonDown ("Interact"))
				{
					interact = true;
				}
				//Y button
				if(CrossPlatformInputManager.GetButtonDown ("Shoot"))
				{
					
				}
				//A button
				if(CrossPlatformInputManager.GetButtonDown ("Shoot"))
				{
					
				}
				//B button
				if(CrossPlatformInputManager.GetButtonDown ("Shoot"))
				{
					
				}
				//Left Analogue Stick Click
				if(CrossPlatformInputManager.GetButtonDown ("Sprint"))
				{
					sprint = true;
				}
				if (CrossPlatformInputManager.GetButtonUp ("Sprint")) 
				{
					sprint = false;
				}
				return;
			case EControlConstraints.DisableAll:
				return;
			case EControlConstraints.DisableAllExceptCamera: // camera isnt dependant on this calss

				return;
			case EControlConstraints.EnableAllExceptPowers:
				//Check for inputs
				//Left trigger
				if(CrossPlatformInputManager.GetAxis ("Melee") > 0 || CrossPlatformInputManager.GetButtonDown ("Melee"))
				{
					melee = true;
				}
				//Right trigger
				if(CrossPlatformInputManager.GetAxis ("Shoot") > 0 || CrossPlatformInputManager.GetButtonDown ("Shoot"))
				{
					//abilityBehaviours.shootRay();
					shoot = true;
				}

				//X button
				if(CrossPlatformInputManager.GetButtonDown ("Interact"))
				{
					interact = true;
				}
				//Y button
				if(CrossPlatformInputManager.GetButtonDown ("Shoot"))
				{
					
				}
				//A button
				if(CrossPlatformInputManager.GetButtonDown ("Shoot"))
				{
					
				}
				//B button
				if(CrossPlatformInputManager.GetButtonDown ("Shoot"))
				{
					
				}
				//Left Analogue Stick Click
				if(CrossPlatformInputManager.GetButtonDown ("Sprint"))
				{
					sprint = true;
				}
				if (CrossPlatformInputManager.GetButtonUp ("Sprint")) 
				{
					sprint = false;
				}

				return;
		}

		/*
		//Check for inputs
		//Left trigger
		if(CrossPlatformInputManager.GetAxis ("Melee") > 0 || CrossPlatformInputManager.GetButtonDown ("Melee"))
		{
			melee = true;
		}
		//Right trigger
		if(CrossPlatformInputManager.GetAxis ("Shoot") > 0 || CrossPlatformInputManager.GetButtonDown ("Shoot"))
		{
			//abilityBehaviours.shootRay();
			shoot = true;
		}
		//Left bumper
		if(CrossPlatformInputManager.GetButtonDown ("PrevPower"))
		{
			prevPower = true;
		}
		//Right bumper
		if(CrossPlatformInputManager.GetButtonDown ("NextPower"))
		{
			nextPower = true;
		}
		//X button
		if(CrossPlatformInputManager.GetButtonDown ("Interact"))
		{
			interact = true;
		}
		//Y button
		if(CrossPlatformInputManager.GetButtonDown ("Shoot"))
		{
			
		}
		//A button
		if(CrossPlatformInputManager.GetButtonDown ("Shoot"))
		{
			
		}
		//B button
		if(CrossPlatformInputManager.GetButtonDown ("Shoot"))
		{
			
		}
		//Left Analogue Stick Click
		if(CrossPlatformInputManager.GetButtonDown ("Sprint"))
		{
			sprint = true;
		}
		if (CrossPlatformInputManager.GetButtonUp ("Sprint")) 
		{
			sprint = false;
		}*/
		
	}

	public bool isSkip
	{
		get{ return skip; }
	}

	public bool isShoot(){
		return shoot;
	}

	public bool isMelee(){
		return melee;
	}

	public bool isSprint(){
		return sprint;
	}

	public bool isPrevPower(){
		return prevPower;
	}

	public bool isNextPower(){
		return nextPower;
	}

	public bool isInteract(){
		return interact;
	}
}

