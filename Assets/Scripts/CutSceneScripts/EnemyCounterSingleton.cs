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
				return null;
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

	private bool eventCompleted = false;

	[SerializeField] private WhiteFlash flash; 
	[SerializeField] private Transform dreamer;
	[SerializeField] private Transform dreamerDestination;
	[SerializeField] private Transform playerTransform;
	[SerializeField] private Vector3 playerOffsetFromDreamer;

	[SerializeField] private GameObject choicePrefab;
	private void Start()
	{
		instance = this;
	}

	private void Update()
	{
		if( currentEnemyCount == 0 && spawnersSpawning == 0 && !eventCompleted)
		{
			StartCoroutine( WaitForTime() );
			eventCompleted = true;
		}
	}

	private IEnumerator WaitForTime()
	{
		flash.FadeToWhite();

		yield return new WaitForSeconds(flash.fadeOutSpeed);

		dreamer.position = dreamerDestination.position;

		playerTransform.position = dreamer.position - playerOffsetFromDreamer;

		Instantiate( choicePrefab, Vector3.zero, Quaternion.identity );

		flash.FadeToClear();

		yield return new WaitForSeconds(flash.fadeInSpeed);

		// stop input?
	}
}
