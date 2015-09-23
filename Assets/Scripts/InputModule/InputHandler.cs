using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;

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

//	AbilityBehaviours abilityBehaviours;
//	FirstPersonController firstPersonController;
//	PunchingControllerPrototype punchingControllerPrototype;

	void Start()
	{
//		abilityBehaviours = (AbilityBehaviours)gameObject.GetComponent ("AbilityBehaviours");
//		firstPersonController = (FirstPersonController)gameObject.GetComponent ("FirstPersonController");
//		punchingControllerPrototype = (PunchingControllerPrototype)gameObject.GetComponent ("PunchingControllerPrototype");
	}

	void Update()
	{
		//Make all triggers false
		shoot = false;
		melee = false;
		sprint = false;

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
		if(CrossPlatformInputManager.GetButtonDown ("Shoot"))
		{
			
		}
		//Right bumper
		if(CrossPlatformInputManager.GetButtonDown ("Shoot"))
		{
			
		}
		//X button
		if(CrossPlatformInputManager.GetButtonDown ("Melee"))
		{
			//punchingControllerPrototype.AttackEvent();
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
			shoot = true;
		}

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
}

