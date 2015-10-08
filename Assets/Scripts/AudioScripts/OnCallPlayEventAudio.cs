using UnityEngine;

public class OnCallPlayEventAudio : MonoBehaviour, IDestroyAudioEvent
{
	[SerializeField] private AudioClip[] audioClip;
	[SerializeField] private float waitTimeBetweenClips;

	private NarratorController narrator;

	private float startTime;

	private int audioClipIndex = 0;
	
	private InputHandler inputHandler;
	
	private EAudioState myState = EAudioState.isWaiting;

	private bool triggerEvent = false;

	private void OnEnable()
	{
		narrator = NarratorController.NarratorInstance;

		inputHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<InputHandler>();
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
			TriggerEvent = true;

		if(myState == EAudioState.isWaiting || myState == EAudioState.isFinished || triggerEvent == false)
			return;

		if(myState == EAudioState.isPaused)
		{
			if( Time.time > (startTime + waitTimeBetweenClips))
			{
				if( ++audioClipIndex == audioClip.Length )
				{
					myState = EAudioState.isFinished;
					triggerEvent = false;
					return;
				}
				else
				{
					myState = EAudioState.isPlaying;
					startTime = Time.time;
					narrator.PlayNewClip(audioClip[audioClipIndex], gameObject.GetInstanceID(), (IDestroyAudioEvent)this);
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

	public bool TriggerEvent
	{
		get {return triggerEvent;}

		set 
		{
			if (value == false)
				return;

			triggerEvent = true;

			myState = EAudioState.isPlaying;
			startTime = Time.time;

			narrator.PlayNewClip(audioClip[audioClipIndex], gameObject.GetInstanceID(), (IDestroyAudioEvent)this);
		}
	}

	public void DestroyAudioEvent()
	{
		Destroy(this);
	}
}
