﻿using UnityEngine;
using System.Collections;

public class OnDestructionTrigger : MonoBehaviour, IDestroyAudioEvent {

	private EnemyScript enemyHealth;
	private Shield shieldScript;
	private OnCallPlayEventAudio myAudioEvent;

	[Tooltip("Objects To Destroy In Case This Gets Triggered First")]
	[SerializeField] private GameObject dependencies;

	// Use this for initialization
	void Start () {

		myAudioEvent = GetComponent<OnCallPlayEventAudio>();
		shieldScript = GetComponent<Shield>();
		enemyHealth = GetComponent<EnemyScript>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(shieldScript != null)
		{
			if(shieldScript.ShieldHealth <= 0)
			{
				myAudioEvent.TriggerEvent = true;
			}
		}


		if(enemyHealth != null)
		{
			if(enemyHealth.Health <= 0)
			{
				if(myAudioEvent != null && myAudioEvent.enabled)
				{
					myAudioEvent.TriggerEvent = true;
				}
				DestroyAudioEvent();
			}
		}
	}

	public void DestroyAudioEvent()
	{
		Destroy(dependencies);
		Destroy(this);
	}
}
