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
	
	[SerializeField] private WhiteFlash flash; 
	[SerializeField] private Transform dreamer;
	[SerializeField] private Transform dreamerDestination;
	[SerializeField] private Transform playerTransform;
	[SerializeField] private Vector3 playerOffsetFromDreamer;

	private float startTime = 0;
	private float waitTime = 10;

	private float fadeoutTime = 0;
	private float fadeinTime = 0;

	[SerializeField] private AudioClip[] audioClips;
	private bool choice = false;

	[SerializeField] private GameObject choicePrefab;

	InputHandler inputHandler;
	private void Awake()
	{
		instance = this;
		startTime = Time.time;

		inputHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<InputHandler>();
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.RightControl))
			StartCutscene();

		/*
		if(Time.time < (startTime + waitTime))
			return;
*/
		if(choice == true && inputHandler.DPadHorizontal != 0)
		{
			StartCoroutine( CutscenePartII() );
			choice = false;
		}

	/*	if( currentEnemyCount == 0 && spawnersSpawning == 0 && !eventCompleted)
		{
			StartCoroutine( WaitForTime() );
			eventCompleted = true;
		}*/
	}

	public void StartCutscene()
	{
		StartCoroutine( FinalCutscene() );
	}

	private IEnumerator FinalCutscene()
	{
		flash.FadeToWhite();

		yield return new WaitForSeconds(fadeoutTime);

		dreamer.position = dreamerDestination.position;

		playerTransform.position = dreamer.position - playerOffsetFromDreamer;
		playerTransform.transform.LookAt (dreamer);
		Instantiate( choicePrefab, Vector3.zero, Quaternion.identity );

		inputHandler.ControllerConstraints = EControlConstraints.DisableAllExceptChoice;

		flash.FadeToClear();

		yield return new WaitForSeconds(fadeinTime);

		NarratorController.NarratorInstance.PlayNewClip(audioClips[0]);

		yield return new WaitForSeconds( audioClips[0].length + 1f );

		choice = true;



		// stop input?
	}

	private IEnumerator CutscenePartII()
	{
		NarratorController.NarratorInstance.PlayNewClip(audioClips[1]);
		yield return new WaitForSeconds( audioClips[1].length + 1f );

		// player touches

		flash.RequestFlash();

		NarratorController.NarratorInstance.PlayNewClip(audioClips[2]);
		yield return new WaitForSeconds( audioClips[2].length + 1f );


		Application.LoadLevel("SHIP");
	}
}
