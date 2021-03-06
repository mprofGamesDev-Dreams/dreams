﻿using UnityEngine;
using System.Collections;

public class SpawnEnemyOnPlatform : MonoBehaviour
{
	// Prefab of object to spawn
	public GameObject EnemyToSpawn;
	public Vector3 PositionOffset = new Vector3 (0.0f, 4.5f, 0.0f);

	// Which waypoint of the movement to spawn the enemy
	public int WaypointID = 0;

	// Access to script
	private TransformGeometry Transformer;

	// Flag for whether or not the enemy can resume AI
	private bool CanMove = false;

	public Transform islandStart;
	public EnemyScript enemyTrigger;
	public GameObject EnemyContainer;
	public bool HasEnemy;

	public GameObject Enemy;

	void Start ()
	{
		// Access the transform geometry script
		Transformer = this.gameObject.GetComponent<TransformGeometry> ();

		// Create the enemy
		Enemy = GameObject.Instantiate (EnemyToSpawn, this.gameObject.transform.position + PositionOffset, new Quaternion ()) as GameObject;
		//Enemy = GameObject.Instantiate (EnemyToSpawn, this.gameObject.transform.position + PositionOffset, new Quaternion ()) as GameObject;
		
		// Set the enemy to transform
		Enemy.transform.parent = this.gameObject.transform;
		Enemy.name = "Enemy";

		// Stop the AI
		NavMeshAgent agent = Enemy.GetComponent<NavMeshAgent> ();
		if(agent.isOnNavMesh && agent.isActiveAndEnabled)
			agent.Stop ();

		// Translate island's children by start offset
		Transform EndPos = this.gameObject.transform.GetChild(1);
		EndPos.position -= islandStart.localPosition;
		
		// Set position to start position
		transform.position = islandStart.position;
		
		// Translate children by start offset
		Transform StartPos = this.gameObject.transform.GetChild(0);
		Destroy (StartPos.gameObject);
		
		// Flag we had an enemy at creation
		if (enemyTrigger)
			HasEnemy = true;
	}

	void Update ()
	{
		// Check if the transformer has reached target
		if (Transformer.CheckPosition (WaypointID))
		{
			if( !CanMove )
			{
				// Resume AI
				if(Enemy != null)
				{
					Enemy.GetComponent<NavMeshAgent> ().Resume ();

					CanMove = true;
				}
			}
		}
		else 
		{
			// Stick the enemy on middle of platform
			if(Enemy != null)
				Enemy.transform.position = this.gameObject.transform.position + PositionOffset;
		}
	}
}
