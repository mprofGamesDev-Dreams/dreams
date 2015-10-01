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
	bool pause;
	bool imagiPower;
	bool voidPower;
	bool logiaPower;
	bool nextPower;
	bool prevPower;

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
		pause = false;
		imagiPower = false;
		voidPower = false;
		logiaPower = false;
		nextPower = false;
		prevPower = false;


		switch (controllerConstraints)
		{
			case EControlConstraints.EnableAll:
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

