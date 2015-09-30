using UnityEngine;
using System.Collections;

/*
 * Script for resetting a gameobjects position on player death
 * 
 * ATTACHMENT: Add to a gameobject prefab
 * 
 */

public class ResetOnPlayerDeath : MonoBehaviour
{
	private GameObject Player;
	private PlayerStats Stats;
	private Vector3 StartingPosition;

	// Use this for initialization
	void Start ()
	{
		// Find the player
		Player = GameObject.Find ("Player");

		// Get access to stats
		Stats = Player.GetComponent<PlayerStats>();

		// Store the initial state
		StartingPosition = this.gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// If the player is dead
		if(Stats.IsDead)
		{
			// Reset the gameobject
			this.gameObject.transform.position = StartingPosition;
		}
	}
}
