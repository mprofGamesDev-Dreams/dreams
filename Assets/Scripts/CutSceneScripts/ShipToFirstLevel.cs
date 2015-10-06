using UnityEngine;
using System.Collections;

using UnityStandardAssets.CrossPlatformInput;

public class ShipToFirstLevel : MonoBehaviour 
{
	[SerializeField] private AudioClip[] audioClips;
	private int audioClipIndex = 0;

	private GameObject playerObject;
	private InputHandler playerInputManager;

	//private AudioSource narrator;

	[SerializeField]private LoadSceneOnTrigger trigger;
	private BoxCollider colliderToTrigger;

	private InputHandler inputHandler;

	private bool terminate;
	private float startTime;
	[SerializeField]private float waitTimeBetweenClips;

	private EAudioState myState = EAudioState.isWaiting;
	private NarratorController narrator;

	private void Start()
	{
		playerObject = GameObject.FindGameObjectWithTag("Player");
		playerInputManager = playerObject.GetComponent<InputHandler>();

		playerInputManager.ControllerConstraints = EControlConstraints.DisableAllExceptCamera;

		playerObject.GetComponent<AbilityBehaviours>().CurrentPower = ActivePower.Logio;



		narrator = NarratorController.NarratorInstance;
		narrator.PlayNewClip(audioClips[audioClipIndex]);

		myState = EAudioState.isPlaying;

		startTime = Time.time;

		inputHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<InputHandler>();

		colliderToTrigger = trigger.GetComponent<BoxCollider>();
	}

	private void Update()
	{
		if(myState == EAudioState.isWaiting)
			return;

		if(myState == EAudioState.isFinished)
		{
			if( !colliderToTrigger.isTrigger )
			{
				colliderToTrigger.isTrigger = true;
			}
			return;
		}
		
		if(myState == EAudioState.isPaused)
		{
			if( Time.time > (startTime + waitTimeBetweenClips))
			{
				if(audioClipIndex == 2)
					return;


				if(audioClipIndex == 3) // Out Of Cutscene Bounds
				{
					myState = EAudioState.isFinished;
					trigger.GetComponent<BoxCollider>().isTrigger = true;
					//Destroy(this);
				}

				if(audioClipIndex == 1)
				{
					playerInputManager.ControllerConstraints = EControlConstraints.EnableAllExceptPowers;
					narrator.PlayNewClip(audioClips[audioClipIndex]);
					myState = EAudioState.isPlaying;
				}
				

				startTime = Time.time;
			}// If Timer Hasnt Finished Then Wait
		}
		
		if(myState == EAudioState.isPlaying)
		{
			if(audioClipIndex == audioClips.Length)
			{
				myState = EAudioState.isFinished;
				return;
			}

			if(Time.time < (startTime + audioClips[audioClipIndex].length))
			{
				// Figure out a button to skip (select/back maybe?)
				if( inputHandler.isSkip )
				{
					narrator.Stop();
					startTime = Time.time + 0.25f;
					myState = EAudioState.isPaused;
					audioClipIndex++;
					return;
				}
				
			}
			
			if(Time.time > (startTime + audioClips[audioClipIndex].length))
			{
				startTime = Time.time;
				myState = EAudioState.isPaused;
				audioClipIndex++;
			}
		}

	}

	public void PlayClip(int i) // played after shooting
	{
		narrator.PlayNewClip(audioClips[i]);
		myState = EAudioState.isPlaying;
		startTime = Time.time;
	}

}

