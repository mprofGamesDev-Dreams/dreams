using UnityEngine;
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

	private GameObject Enemy;

	void Start ()
	{
		// Access the transform geometry script
		Transformer = this.gameObject.GetComponent<TransformGeometry> ();

		// Create the enemy
		Enemy = GameObject.Instantiate (EnemyToSpawn, this.gameObject.transform.position + PositionOffset, new Quaternion ()) as GameObject;
		
		// Set the enemy to transform
		Enemy.transform.parent = this.gameObject.transform;
		Enemy.name = "Enemy";

		// Stop the AI
		NavMeshAgent agent = Enemy.GetComponent<NavMeshAgent> ();
		if(agent.isOnNavMesh && agent.isActiveAndEnabled)
			agent.Stop ();
	}

	void Update ()
	{
		// Check if the transformer has reached target
		if (Transformer.CheckPosition (WaypointID))
		{
			if( !CanMove )
			{
				// Resume AI
				Enemy.GetComponent<NavMeshAgent> ().Resume ();
				CanMove = true;
			}
		}
		else 
		{
			// Stick the enemy on middle of platform
			Enemy.transform.position = this.gameObject.transform.position + PositionOffset;
		}
	}
}
