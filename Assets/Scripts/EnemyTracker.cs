using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyTracker : MonoBehaviour
{
	public string EnemyNamePrefix = "Enemy";

	private List<GameObject> EnemyList = new List<GameObject>();

	void Start ()
	{
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
