using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.ImageEffects;
using UnityStandardAssets.Characters.FirstPerson;

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

	[SerializeField] private GameObject ChoiceObject;

	//private float startTime = 0;
	//private float waitTime = 10;

	private Button currentlySelectedButton = null;

//	private float fadeoutTime = 0;
	private float fadeinTime = 0;

	[SerializeField] private AudioClip[] audioClips;
	private bool choice = false;


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
		if(choice == true && inputHandler.DPadHorizontal != 0 || inputHandler.HorizontalAxis != 0)
		{
			float dir = 0;
			if(inputHandler.DPadHorizontal != 0)
				dir = inputHandler.DPadHorizontal;
			else dir = inputHandler.HorizontalAxis;

			if(dir > 0)
			{
				if(currentlySelectedButton != ChoiceObject.GetComponent<RectTransform>().GetChild(0).GetComponent<Button>())
				{
					currentlySelectedButton = ChoiceObject.GetComponent<RectTransform>().GetChild(0).GetComponent<Button>();
					currentlySelectedButton.Select();
				}
			}

			if(dir < 0)
			{
				if(currentlySelectedButton != ChoiceObject.GetComponent<RectTransform>().GetChild(1).GetComponent<Button>())
				{
					currentlySelectedButton = ChoiceObject.GetComponent<RectTransform>().GetChild(1).GetComponent<Button>();
					currentlySelectedButton.Select();
				}
			}

			/*
			StartCoroutine( CutscenePartII() );
			ChoiceObject.SetActive(false);
			choice = false;
			*/
		}


        Debug.Log( choice + ", " + inputHandler.isInteract() + ", " + currentlySelectedButton );

		if(choice == true && inputHandler.isInteract() && currentlySelectedButton != null)
		{
			ChoiceObject.SetActive(false);
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
		flash.fadeInSpeed = 2;
		flash.fadeOutSpeed = 2;
		flash.RequestFlash();

		yield return new WaitForSeconds( 3 );

		dreamer.position = dreamerDestination.position;

		playerTransform.position = dreamer.position - playerOffsetFromDreamer;

		Transform sceneCamera = TurnOnSceneCamera(true);

		playerTransform.GetComponent<FirstPersonController>().enabled = false;

		sceneCamera.LookAt(dreamer.position);

		//playerTransform.forward = (dreamer.position - playerTransform.position).normalized;;

		//Instantiate( choicePrefab, Vector3.zero, Quaternion.identity );

		inputHandler.ControllerConstraints = EControlConstraints.DisableAllExceptChoice;


		yield return new WaitForSeconds(fadeinTime);
		ChoiceObject.SetActive(true);

		NarratorController.NarratorInstance.PlayNewClip(audioClips[0], gameObject.GetInstanceID(), (IDestroyAudioEvent)this);

		yield return new WaitForSeconds( audioClips[0].length + 1f );

		choice = true;
	}

	private IEnumerator CutscenePartII()
	{

		ChoiceObject.SetActive(false);
		NarratorController.NarratorInstance.PlayNewClip(audioClips[1], gameObject.GetInstanceID(), (IDestroyAudioEvent)this);
		
       
        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
        tempPlayer.GetComponent<InputHandler>().PlayInteract();


        yield return new WaitForSeconds( audioClips[1].length + 1f );


		flash.fadeInSpeed = 7f;
		flash.fadeOutSpeed = 0.0000000000001f;
		flash.RequestFlash();

		NarratorController.NarratorInstance.PlayNewClip(audioClips[2], gameObject.GetInstanceID(), (IDestroyAudioEvent)this);
		yield return new WaitForSeconds( audioClips[2].length + 1f );

		// Wait A 5 Beats As Requested
		yield return new WaitForSeconds( 2.5f );

        ChoiceObject.transform.parent.GetComponent<LoadToScene>().OnClickLoadSceneByString("SHIPFadeIn");
		//currentlySelectedButton.onClick.Invoke();
		//currentlySelectedButton..Invoke();
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

		BloomOptimized sceneBloom = camObj.AddComponent<BloomOptimized>();
		
		BloomOptimized mainBloom = Camera.main.GetComponent<BloomOptimized>();
		
		sceneBloom.threshold = mainBloom.threshold;
		sceneBloom.intensity = mainBloom.intensity;
		sceneBloom.blurSize = mainBloom.blurSize;
		sceneBloom.blurIterations = mainBloom.blurIterations;
		sceneBloom.blurType = mainBloom.blurType;
		sceneBloom.fastBloomShader = mainBloom.fastBloomShader;

		return sceneCamera.GetComponent<Camera>();
	}

	public void DestroyAudioEvent()
	{

	}
}
