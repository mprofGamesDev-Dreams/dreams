using UnityEngine;
using System.Collections;

/*
 * Script for creating copies of an enemy when it dies
 * 
 * ATTACHMENT: Add to parent enemy.
 * 
 */

public class EnemyDeath : MonoBehaviour
{
	// Which prefab to use for the new enemies
	public GameObject SplitEnemyObject;

	// How many enemies to create on death
	public int NumberOfSplits;

	// How big the new enemies should be
	public Vector3 SplitEnemySize = new Vector3(0.5f, 0.5f, 0.5f);

	// Access to this game object
	private EnemyScript EnemyInfo;
	
	// Use this for initialization
	void Start ()
	{
		// Get access to the enemy script
		EnemyInfo = GetComponent<EnemyScript>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// If the enemy is dead
		if(EnemyInfo.Health <= 0)
		{
			// Loop through and create enemies
			for(int i = 0; i < NumberOfSplits; i++)
			{
				// Generate a random offset
				float xOffset = Random.Range (-2,2);
				float yOffset = Random.Range (-2,2);
				float zOffset = Random.Range (-2,2);

				Vector3 Offset = new Vector3(xOffset, yOffset, zOffset);

				// Create a copy of the enemy near the death location
				GameObject temp = GameObject.Instantiate(SplitEnemyObject, transform.position + Offset, transform.rotation) as GameObject;

				// Reduce size
				temp.transform.localScale = SplitEnemySize;
			}
		}
	}
}
