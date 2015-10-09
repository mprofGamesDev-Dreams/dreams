using UnityEngine;
using System.Collections;

public class OnEnemyDeathTrigger : MonoBehaviour, IDestroyAudioEvent {

	[SerializeField] private EnemyScript enemyHealth;
	OnCallPlayEventAudio myAudioEvent;

	// Use this for initialization
	void Start () {

		myAudioEvent = GetComponent<OnCallPlayEventAudio>();
	}
	
	// Update is called once per frame
	void Update () {
		if(enemyHealth.Health <= 0)
		{
			if(myAudioEvent != null && myAudioEvent.enabled)
			{
				myAudioEvent.TriggerEvent = true;
			}
			Destroy(this);
		}
	}

	public void DestroyAudioEvent()
	{
		Destroy(this);
	}
}
