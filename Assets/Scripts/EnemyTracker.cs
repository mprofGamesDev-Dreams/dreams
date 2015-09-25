using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Script to track enemies. 
 * 
 * ATTACHMENT: Attach to sound manager object.
 * 
 * VARIABLES:
 * 		EnemyNamePrefix - The standard name of enemies
 */

public class EnemyTracker : MonoBehaviour
{
	public string EnemyNamePrefix = "Enemy";

	private List<GameObject> EnemyList = new List<GameObject>();

	void Start ()
	{
		// Add enemies to the enemy list
		foreach(GameObject go in GameObject.FindObjectsOfType(typeof(GameObject)))
		{
			if(go.name.Contains(EnemyNamePrefix))
				EnemyList.Add (go);
		}
	}

	public bool CheckTracking()
	{
		// Loop through each enemy
		// If any enemy is currently tracking the player return true
		foreach(GameObject Enemy in EnemyList)
		{
			if(Enemy.GetComponent<EnemyBehaviour>().GetPlayerSeen())
			{
				return true;	
			}
		}

		return false;
	}
}
