using UnityEngine;
using System.Collections;

public class EnemySpawning : MonoBehaviour {

	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private float spawnPause;
	[SerializeField] private int spawnAmount;
	[SerializeField] private bool spawnOnStart;

	private bool canSpawn;
	private float startTime;

	private void Start () 
	{
		if (spawnOnStart) 
		{
			canSpawn = true;
		}
		if(EnemyCounterSingleton.Instance != null)
			EnemyCounterSingleton.Instance.SpawnersSpawning++;
	}
	
	private void Update () 
	{
		if(canSpawn)
		{
			if( Time.time > startTime + spawnPause && spawnAmount > 0 )
			{
				Instantiate( enemyPrefab, transform.position, Quaternion.identity );
				startTime = Time.time;
				spawnAmount--;
			}

			
			if (spawnAmount <= 0)
			{
				canSpawn = false;
				if(EnemyCounterSingleton.Instance != null)
					EnemyCounterSingleton.Instance.SpawnersSpawning--;
			}
		}

	}

	public bool CanSpawn
	{
		get{ return canSpawn; }
		set{ canSpawn = value; startTime = Time.time; }
	}
}
