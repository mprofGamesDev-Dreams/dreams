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

	[SerializeField] private PlayerStats playerStats;

	[Header("Skill Costs")]
	[SerializeField] [Range(0, 1)] private float logioPercent = 0;
	[SerializeField] [Range(0, 1)] private float voidPercent = 0;
	[SerializeField] [Range(0, 1)] private float imagiPercent = 0;

	[Header("Skill Cooldown")]
	[SerializeField] [Range(0, 5)] private float logioCD = 0;
	[SerializeField] [Range(0, 5)] private float imagiCD = 0;
	[SerializeField] [Range(0, 5)] private float voidCD = 0;

	private bool isLogioAvailable = true;
	private bool isImagiAvailable = true;
	private bool isVoidAvailable = true;

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

		playerStats = GetComponent<PlayerStats>();
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
			playerStats.CurrentImagi -= ( playerStats.MaxImagi * imagiPercent );

			currentPower = ActivePower.Imagi;
		}

		if (CrossPlatformInputManager.GetButtonDown ("Fire3"))//VOID
		{
			currentPower = ActivePower.Void;
		}*/

		// time scale to stop you shooting while pause menu is active
		if (input.isShoot () && Time.timeScale != 0) 
		{
			if( canFire() )
			{
				HandleSkillCosts();
				shootBullet();
			}
		}
	}

	private bool canFire()
	{
		if (currentPower == ActivePower.Imagi && playerStats.CurrentImagi >= ( playerStats.MaxImagi * imagiPercent ) && isImagiAvailable ) 
		{
			return true;
		}

		if (currentPower == ActivePower.Logio && playerStats.CurrentLogio >= ( playerStats.MaxLogio * logioPercent ) && isLogioAvailable ) 
		{
			return true;
		}

		if (currentPower == ActivePower.Void && playerStats.CurrentVoid >= ( playerStats.MaxVoid * voidPercent ) && isVoidAvailable ) 
		{
			return true;
		}

		return false;
	}

	private void HandleSkillCosts()
	{
		switch (currentPower) 
		{
		case ActivePower.Imagi:
			playerStats.CurrentImagi -= ( playerStats.MaxImagi * imagiPercent );
			StartCoroutine( CooldownCounter( imagiCD ) );
			break;
		case ActivePower.Logio:
			playerStats.CurrentLogio -= ( playerStats.MaxLogio * logioPercent );
			StartCoroutine( CooldownCounter( logioCD ) );
			break;
		case ActivePower.Void:
			playerStats.CurrentVoid -= ( playerStats.MaxVoid * voidPercent );
			StartCoroutine( CooldownCounter( voidCD ) );
			break;
		}
	}

	private IEnumerator CooldownCounter( float timeToWait )
	{
		ActivePower power = currentPower;

		switch (power) 
		{
		case ActivePower.Imagi:
			isImagiAvailable = false;
			break;
		case ActivePower.Logio:
			isLogioAvailable = false;
			break;
		case ActivePower.Void:
			isVoidAvailable = false;
			break;
		}

		yield return new WaitForSeconds (timeToWait);

		switch (power) 
		{
		case ActivePower.Imagi:
			isImagiAvailable = true;
			break;
		case ActivePower.Logio:
			isLogioAvailable = true;
			break;
		case ActivePower.Void:
			isVoidAvailable = true;
			break;
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
		GameObject bullet;
		switch (currentPower) 
		{
		case ActivePower.Logio:
			bullet = (GameObject)Instantiate(logioBullet, myCameraTransform.position+(myCameraTransform.forward*1), myCameraTransform.rotation);
			bullet.GetComponent<Bullet>().SetParent(gameObject);
			break;
		case ActivePower.Imagi:
			bullet = (GameObject)Instantiate(imagiBullet, myCameraTransform.position+(myCameraTransform.forward*1), myCameraTransform.rotation); 
			bullet.GetComponent<Bullet>().SetParent(gameObject);
			break;
		case ActivePower.Void:
			bullet = (GameObject)Instantiate(voidBullet, myCameraTransform.position+(myCameraTransform.forward*1), myCameraTransform.rotation); 
			bullet.GetComponent<Bullet>().SetParent(gameObject);
			break;
		}


	}

	public ActivePower getCurrentPower(){
		return currentPower;
	}
}



