﻿using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Narrator controller.
/// 
/// Makes A User Friendly Singleton Object To Link All Scripts Of Type:
/// OnCallPlayEventAudio
/// OnTriggerPlay
/// 
/// To Play From The Same AudioSource 
/// </summary>

public class NarratorController 
{
	// Narrator AudioSource Reference
	private static AudioSource narrator = null; 

	// NarratorController Singleton
	private static NarratorController narratorInstance = null;
	public static NarratorController NarratorInstance 
	{ 
		get 
		{ 
			// Adds A AudioSource To Player For Dedicated Narration
			if(narrator == null)
			{
				narrator = GameObject.FindGameObjectWithTag("Player").AddComponent<AudioSource>();
			}

			// Makes Sure There Is A Singleton Instance Available
			if( narratorInstance == null )
			{
				narratorInstance = new NarratorController();
			}

			return narratorInstance;
		} 
	}
	
	int requestObjectID = -1;
	IDestroyAudioEvent objReference = null;

	// Plays A New Clip
	public void PlayNewClip(AudioClip audioClip, int objID, IDestroyAudioEvent objInterface)
	{
		if( objID != requestObjectID )
		{
			if(objReference != null)
			{
				objReference.DestroyAudioEvent() ;
			}
			
			requestObjectID = objID;
			objReference = objInterface;

			narrator.Stop();

			narrator.clip = audioClip;
			narrator.PlayDelayed(0.3f);
			return;
		}

		narrator.clip = audioClip;
		narrator.Play();
	}

	// Stops Any Playing Clip
	public void Stop()
	{
		narrator.Stop();
	}

	public bool isPlaying()
	{
		return narrator.isPlaying;
	}
}
