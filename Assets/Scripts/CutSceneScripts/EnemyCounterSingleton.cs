using UnityEngine;
using System.Collections;

public class EnemyCounterSingleton : MonoBehaviour 
{
	private static EnemyCounterSingleton instance; 
	public static EnemyCounterSingleton Instance 
	{
		get 
		{
			if(instance == null)
			{
				Debug.LogError("Config A EnemyCounter!");
			}
			return instance;
		}
	}

	private int currentEnemyCount;

	public int CurrentEnemyCount
	{
		get { return currentEnemyCount; }
		set { currentEnemyCount = value; }
	}

	private int spawnersSpawning;

	public int SpawnersSpawning
	{
		get { return spawnersSpawning; }
		set { spawnersSpawning = value; }
	}

	private bool eventCalled = false;

	private void Update()
	{
		if( currentEnemyCount == 0 && spawnersSpawning == 0 && !eventCalled)
		{
			// Call Event;
			;
		}
	}
}
