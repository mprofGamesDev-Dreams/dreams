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

	// Flag for whether or not the it has trigger
	private bool EnemySpawned = false;

	private GameObject Enemy;

	void Start ()
	{
		// Access the transform geometry script
		Transformer = this.gameObject.GetComponent<TransformGeometry> ();

		// Create the enemy
		Enemy = GameObject.Instantiate (EnemyToSpawn, this.gameObject.transform.position + PositionOffset, new Quaternion ()) as GameObject;
		
		// Set the enemy to transform
		Enemy.transform.parent = this.gameObject.transform;

		// Stop the AI
		Enemy.GetComponent<NavMeshAgent> ().Stop ();
	}

	void Update ()
	{
		// If we have already activated, stop
		if (EnemySpawned)
			return;

		// If the platform is still moving
		if (!Transformer.CheckPosition (WaypointID))
		{
			Enemy.transform.position = this.gameObject.transform.position + PositionOffset;
			return;
		}
		
		// Flag we have now spawned
		EnemySpawned = true;

		// Resume AI
		Enemy.GetComponent<NavMeshAgent> ().Resume ();
	}
}
