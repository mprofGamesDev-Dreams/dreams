/*
 * Script that implements magical abilities
 * Must be attached to the player character
 * Must be provided bullet prefabs that contain a "Bullet" script
 */

using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class AbilityBehaviours : MonoBehaviour 
{
	// Serialize Private Fields To Show Up In The Insepctor
	[SerializeField] private float attackRange = 100.0f;
	[SerializeField] private GameObject logioBullet;
	[SerializeField] private GameObject imagiBullet;
	[SerializeField] private GameObject voidBullet;
	[SerializeField] private ParticleSystem myParticleSystem;

	private Transform myCameraTransform;
	private InputHandler input;
	private Color32 beamColor;
	ActivePower currentPower;
	private void Start () 
	{
		// Gets The Main Camera's Transform On Object Startup
		myCameraTransform = Camera.main.GetComponent<Transform>();
		input = gameObject.GetComponent<InputHandler>();
		currentPower = ActivePower.Imagi;
	}
	
	private void Update () 
	{		
		//Switches for next and previous powers
		if (input.isPrevPower ()) {
			switch(currentPower){
			case ActivePower.Logio:
				currentPower = ActivePower.Void;
				break;
			case ActivePower.Imagi:
				currentPower = ActivePower.Logio;
				break;
			case ActivePower.Void:
				currentPower = ActivePower.Imagi;
				break;
			}
		}

		if (input.isNextPower ()) {
			switch(currentPower){
			case ActivePower.Logio:
				currentPower = ActivePower.Imagi;
				break;
			case ActivePower.Imagi:
				currentPower = ActivePower.Void;
				break;
			case ActivePower.Void:
				currentPower = ActivePower.Logio;
				break;
			}
		}

		/*if (CrossPlatformInputManager.GetButtonDown ("Fire1"))//LOGIO
		{
			currentPower = ActivePower.Logio;
		}
		
		if (CrossPlatformInputManager.GetButtonDown ("Fire2"))//IMAGI
		{
			currentPower = ActivePower.Imagi;
		}
		
		if (CrossPlatformInputManager.GetButtonDown ("Fire3"))//VOID
		{
			currentPower = ActivePower.Void;
		}*/

		if (input.isShoot ()) 
		{
			shootBullet();
		}
	}

	public void setActivePower(ActivePower ap)
	{
		currentPower = ap;
	}

	private void shootRay()
	{

		switch(currentPower)
		{
		case ActivePower.Logio:
			beamColor = Color.yellow;
			break;
		case ActivePower.Imagi:
			beamColor = Color.cyan;
			break;
		case ActivePower.Void:
			beamColor = Color.blue;
			break;
		}
		
		RaycastHit hit;
		Ray pointer = new Ray(myCameraTransform.position, myCameraTransform.forward * attackRange);
		
		Debug.DrawRay(pointer.origin, pointer.direction, beamColor);
		
		myParticleSystem.startColor = beamColor;
		myParticleSystem.Emit(1);
		
		if (Physics.Raycast(pointer, out hit, attackRange))
		{
			Shield shieldScript = hit.transform.GetComponent<Shield>();
			if(shieldScript != null)
			{
				switch(currentPower)
				{
				case ActivePower.Logio:
					shieldScript.ApplyDamage(Shield.shieldOptions.Logio, 10	);
					break;
				case ActivePower.Imagi:
					shieldScript.ApplyDamage(Shield.shieldOptions.Imagi, 10	);
					break;
				case ActivePower.Void:
					shieldScript.ApplyDamage(Shield.shieldOptions.Void, 10	);
					break;
					
				}
			}
		}
	}

	private void shootBullet()
	{

		switch (currentPower) 
		{
		case ActivePower.Logio:
			Instantiate(logioBullet, myCameraTransform.position+(myCameraTransform.forward*1), myCameraTransform.rotation);
			break;
		case ActivePower.Imagi:
			Instantiate(imagiBullet, myCameraTransform.position+(myCameraTransform.forward*1), myCameraTransform.rotation); 
			break;
		case ActivePower.Void:
			Instantiate(voidBullet, myCameraTransform.position+(myCameraTransform.forward*1), myCameraTransform.rotation); 
			break;
		}

	}

	public ActivePower getCurrentPower(){
		return currentPower;
	}
}



