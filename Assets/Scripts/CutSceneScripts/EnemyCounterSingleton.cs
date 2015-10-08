using UnityEngine;
using System.Collections;

public class EnemyCounterSingleton : MonoBehaviour, IDestroyAudioEvent
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

	//private float startTime = 0;
	//private float waitTime = 10;

	private float fadeoutTime = 0;
	private float fadeinTime = 0;

	[SerializeField] private AudioClip[] audioClips;
	private bool choice = false;

	[SerializeField] private GameObject choicePrefab;

	InputHandler inputHandler;
	private void Awake()
	{
		instance = this;
		//startTime = Time.time;

		inputHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<InputHandler>();
	}

	private void Update()
	{

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

		Transform sceneCamera = TurnOnSceneCamera(true);

		sceneCamera.LookAt(dreamer.position);

		//playerTransform.forward = (dreamer.position - playerTransform.position).normalized;;

		Instantiate( choicePrefab, Vector3.zero, Quaternion.identity );

		inputHandler.ControllerConstraints = EControlConstraints.DisableAllExceptChoice;

		flash.FadeToClear();

		yield return new WaitForSeconds(fadeinTime);

		NarratorController.NarratorInstance.PlayNewClip(audioClips[0], gameObject.GetInstanceID(), (IDestroyAudioEvent)this);

		yield return new WaitForSeconds( audioClips[0].length + 1f );

		choice = true;
	}

	private IEnumerator CutscenePartII()
	{
		NarratorController.NarratorInstance.PlayNewClip(audioClips[1], gameObject.GetInstanceID(), (IDestroyAudioEvent)this);
		yield return new WaitForSeconds( audioClips[1].length + 1f );

		// player touches

		flash.FadeToWhite();

		NarratorController.NarratorInstance.PlayNewClip(audioClips[2], gameObject.GetInstanceID(), (IDestroyAudioEvent)this);
		yield return new WaitForSeconds( audioClips[2].length + 1f );


		Application.LoadLevel("SHIP");
	}


	
	// Always Returns SceneCamera
	private Transform TurnOnSceneCamera( bool state )
	{
		Camera mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		Camera sceneCamera;
		
		// Make Sure SceneCamera Exists If Not Create It
		GameObject obj;
		if( (obj = GameObject.FindGameObjectWithTag("SceneCamera")) == null )
			sceneCamera = CreateSceneCamera();
		else sceneCamera = obj.GetComponent<Camera>();
		
		if( sceneCamera == null )
			sceneCamera = CreateSceneCamera();
		
		mainCamera.enabled = !state;
		sceneCamera.enabled = state;
		
		return sceneCamera.transform;
	}
	
	private Camera CreateSceneCamera()
	{
		GameObject camObj = new GameObject("SceneCamera", typeof(Camera));
		Transform sceneCamera = camObj.GetComponent<Transform>();
		sceneCamera.tag = "SceneCamera";
		sceneCamera.position = Camera.main.transform.position;
		sceneCamera.rotation = Camera.main.transform.rotation;
		sceneCamera.parent = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		
		sceneCamera.GetComponent<Camera>().cullingMask = Camera.main.cullingMask;
		
		return sceneCamera.GetComponent<Camera>();
	}

	public void DestroyAudioEvent()
	{
		Destroy(this);
	}
}
