using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;

public enum EControlConstraints
{
	EnableAllExceptPowers,
	EnableAll,
	DisableAll,
	DisableAllExceptChoice,
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
	float dPadHorizontal;

	private EControlConstraints controllerConstraints = EControlConstraints.EnableAll; public EControlConstraints ControllerConstraints { get { return controllerConstraints; } set { controllerConstraints = value; }  }

	void Start()
	{
		sprint = false;
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
		dPadHorizontal = 0;


		if(CrossPlatformInputManager.GetButton("Skip"))
		{
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
			case EControlConstraints.DisableAllExceptChoice:
				dPadHorizontal = CrossPlatformInputManager.GetAxis("D-Pad X Axis");
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
	}

	public float DPadHorizontal
	{
		get { return dPadHorizontal; }
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

