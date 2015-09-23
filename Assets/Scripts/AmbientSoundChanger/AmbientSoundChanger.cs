using UnityEngine;
using System.Collections;

/*
 * Script to change the pitch of a sound when the player switches between states
 * 
 */

public class AmbientSoundChanger : MonoBehaviour
{
	// Attack states for the player
	public enum STATE { ATTACK, CALM };

	// Current state the player is in
	public STATE CurrentState = STATE.CALM;

	// How fast to play the song in each state
	public float CalmSongSpeed = 1.0f;
	public float AttackSongSpeed = 1.2f;

	// How fast to lerp between song speeds
	public float PitchChangeSpeed = 0.01f;

	// Ambient Sound
	public AudioClip Song;

	// Speed to play the song at
	private float CurrentSpeed;

	// Access to audio source
	private AudioSource Source;

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
	}

	void Update()
	{
		// Make sure to gradually increase/decrease sound speed
		Source.pitch = Mathf.Lerp(Source.pitch, CurrentSpeed, PitchChangeSpeed * Time.smoothDeltaTime);

		UpdateSpeed();
	}

	private void UpdateSpeed()
	{
		// Set the current speed the song should play at
		switch(CurrentState)
		{
			case STATE.ATTACK:
				CurrentSpeed = AttackSongSpeed;
				break;

			case STATE.CALM:
				CurrentSpeed = CalmSongSpeed;
				break;
		}
	}

	public void SetState(STATE NewState)
	{
		// Set the new state
		CurrentState = NewState;

		// Change the speed
		UpdateSpeed();
	}
}

