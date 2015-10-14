using UnityEngine;
using System.Collections;

public class OnCollisionPlay : MonoBehaviour 
{
	[SerializeField]private AudioClip clip = null;
	[SerializeField]private AudioSource sfxSource = null;

	private EAudioState myState = EAudioState.isWaiting;

	private float startTime = 0;

	private void Update () 
	{
		if( myState == EAudioState.isPlaying && Time.time > (startTime + clip.length) )
			myState = EAudioState.isPaused;
	}

	private void OnTriggerEnter(Collider obj)
	{
		if(  obj.gameObject.CompareTag("Player"))
		{
			if( myState == EAudioState.isPaused || myState == EAudioState.isWaiting )
			{
				sfxSource.clip = clip;
				sfxSource.Play();
			}
		}
	}
}
