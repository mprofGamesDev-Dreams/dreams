using UnityEngine;
using System.Collections;

/*
 * Script to play a sound on entering a trigger area
 * 
 * ATTACHMENT: Add to a empty game object. Set the collider to IsTrigger. Specify a sound clip and your good to go.
 * 
 */

public class SoundTrigger : MonoBehaviour
{
	// Sound file to play
	public AudioClip Clip;

	// How many times can it be played
	public int MaxPlayCount = 1;

	// Total plays
	private int PlayCount = 0;

	// Access to audio player
	private AudioSource Source;
	
	void Start ()
	{
		// Attach a player to the trigger
		Source = this.gameObject.AddComponent<AudioSource>();
		Source.clip = Clip;
	}

	void OnTriggerEnter(Collider col )
	{
		// Just double check as we dont want to play
		if(col.gameObject.name == "Player" && !Source.isPlaying)
		{
			// Max sure we can play it
			if( PlayCount >= MaxPlayCount)
				return;

			// Play 
			PlayCount++;
			Source.Play();
		}
	}
}
