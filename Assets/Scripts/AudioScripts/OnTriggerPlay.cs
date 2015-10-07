using UnityEngine;
using System.Collections;

public enum EAudioState
{
	isWaiting, // Waiting On Trigger
	isPlaying, // Playing Audio
	isPaused,  // Pause Between AudioClips
	isFinished // Finished Playing All Clips
}

public class OnTriggerPlay : MonoBehaviour 
{
	[SerializeField] private AudioClip[] audioClip;
	[SerializeField] private float waitTimeBetweenClips;

	private NarratorController narrator = null;

	private float startTime;

	private int audioClipIndex = 0;
	
	private InputHandler inputHandler;
	
	private EAudioState myState = EAudioState.isWaiting;

	private void Start()
	{
		narrator = NarratorController.NarratorInstance;

		inputHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<InputHandler>();
	}

	private void Update()
	{
		if(myState == EAudioState.isWaiting || myState == EAudioState.isFinished)
			return;

		if(myState == EAudioState.isPaused)
		{
			if( Time.time > (startTime + waitTimeBetweenClips))
			{
				if( ++audioClipIndex == audioClip.Length )
				{
					myState = EAudioState.isFinished;
					return;
				}
				else
				{
					myState = EAudioState.isPlaying;
					startTime = Time.time;
					narrator.PlayNewClip(audioClip[audioClipIndex]);

					return;
				}
			}// If Timer Hasnt Finished Then Wait
		}

		if(myState == EAudioState.isPlaying)
		{
			if(Time.time < (startTime + audioClip[audioClipIndex].length))
			{
				// Figure out a button to skip (select/back maybe?)
				if( inputHandler.isSkip )
				{
					narrator.Stop();
					startTime = Time.time + 0.25f;
					myState = EAudioState.isPaused;
					return;
				}
				
			}

			if(Time.time > (startTime + audioClip[audioClipIndex].length))
			{
				startTime = Time.time;
				myState = EAudioState.isPaused;
			}
		}
	}

	private void OnTriggerEnter(Collider obj)
	{
		if(obj.gameObject.CompareTag("Player") && myState == EAudioState.isWaiting )
		{
			myState = EAudioState.isPlaying;
			startTime = Time.time;

			narrator.PlayNewClip(audioClip[audioClipIndex]);
		}
	}

	public EAudioState CurrentState
	{
		get { return myState; }
	}
}
