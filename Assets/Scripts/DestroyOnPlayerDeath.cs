using UnityEngine;
using System.Collections;

/*
 * Script for destroying a gameobjects position on player death
 * 
 * ATTACHMENT: Add to a gameobject prefab
 * 
 */

public class DestroyOnPlayerDeath : MonoBehaviour
{
	private GameObject Player;
	private PlayerStats Stats;
	
	// Use this for initialization
	void Start ()
	{
		// Find the player
		Player = GameObject.Find ("Player");
		
		// Get access to stats
		Stats = Player.GetComponent<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// If the player is dead
		if(Stats.IsDead)
		{
			// Destroy self
			Destroy (this.gameObject);
		}
	}
}
