using UnityEngine;
using System.Collections;

/*
 * Script for handling stats
 * 
 * ATTACHMENT: Add to the player object
 * 
 */

public class PlayerStats : MonoBehaviour
{
	// Access to the player object
	private GameObject Player;
	private CharacterController Controller; 

	// List the stats
	public float Health = 100.0f;
	public float Power = 100.0f;
	public float Stamina = 100.0f;
	public float Particles = 100.0f;

	// Flags
	public bool IsDead = false;

	// Threshold
	private int StaminaThreshold = 10;

	// Rejuvenation
	private float StaminaRecoverySpeed = 0.5f;

	// Maximum values
	public int HealthMax = 100;
	public int PowerMax = 100;
	public int StaminaMax = 100;
	public int ParticlesMax = 100;

	//Starting values
	public int HealthStart = 100;
	public int PowerStart = 100;
	public int StaminaStart = 100;
	public int ParticlesStart = 100;
	
	void Start ()
	{
		// Find the player game object
		Player = GameObject.Find("Player");

		Controller = Player.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		HandleHealth();
		HandleStamina ();
		HandleParticles();
		HandlePower ();
	}

	private void HandleStamina()
	{
		// If the player is sprinting
		// Reduce stamina
		// If stamina is below threshold
		// Damage player?
	}

	private void HandleHealth()
	{
		if( IsDead )
		{
			// Do something later with this?
			return;
		}
		
		// Check player health to see if death has occured
		if( Health <= 0 )
			IsDead = true;
	}

	private void HandleParticles()
	{
	}

	private void HandlePower()
	{
	}
}
