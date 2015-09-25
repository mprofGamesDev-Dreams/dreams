using UnityEngine;
using System.Collections;

/*
 * Script to change the pitch of a sound when the player switches between states
 * 
 * ATTACHMENT: Add to a global manager. Requires AudioSource and EnemyTracker scripts to also be added.
 *
 * VARIABLES:
 * 			NormalSongSpeed - How fast the music should play normal
 * 			BattleSongSpeed - How fast the music should play in battle
 * 			PitchChangeSpeed - How fast to lerp the current pitch to the new pitch
 * 			Song - The soundclip to play
 */

public class AmbientSoundChanger : MonoBehaviour
{
	// Battle states for the player
	public enum STATE { BATTLE, NORMAL };

	// Current state the player is in
	public STATE CurrentState = STATE.NORMAL;

	// How fast to play the song in each state
	public float NormalSongSpeed = 1.0f;
	public float BattleSongSpeed = 1.5f;

	// How fast to lerp between song speeds
	public float PitchChangeSpeed = 0.1f;

	// Ambient Sound
	public AudioClip Song;

	// Speed to play the song at
	private float CurrentSpeed;

	// Access to audio source
	private AudioSource Source;

	// Access to enemy manager
	private EnemyTracker Tracker;

	void Start ()
	{
		// Get the audio source
		Source = GetComponent<AudioSource>();

		// Set the song and set to loop
		Source.clip = Song;
		Source.loop = true;

		// Set initial speed
		UpdateSpeed();

		// Set audio source speed
		Source.pitch = CurrentSpeed;

		// Play the sound
		Source.Play();

		// Get the enemy tracker
		Tracker = GetComponent<EnemyTracker>();
	}

	void Update()
	{
		// Check if any objects are currently tracking the player
		HandleEnemyTracking();

		// Make sure to gradually increase/decrease sound speed
		Source.pitch = Mathf.Lerp(Source.pitch, CurrentSpeed, PitchChangeSpeed * Time.smoothDeltaTime);
	}

	
	private void HandleEnemyTracking()
	{
		// Check if any enemies are tracking the player
		if( Tracker.CheckTracking() )
		{
			// Update the flag to Battle
			SetState(STATE.BATTLE);
		}
		else
		{
			// Not being tracked so reduce speed of music
			SetState(STATE.NORMAL);
		}
	}

	private void UpdateSpeed()
	{
		// Set the current speed the song should play at
		switch(CurrentState)
		{
			case STATE.BATTLE:
				CurrentSpeed = BattleSongSpeed;
				break;
				
			case STATE.NORMAL:
				CurrentSpeed = NormalSongSpeed;
				break;
		}
	}
	
	public void SetState(STATE NewState)
	{
		// If we are already in that state, dont update
		if( CurrentState != NewState )
		{
			// Update to new state
			CurrentState = NewState;
			
			// Set the new song state speed
			UpdateSpeed ();
		}
	}
}