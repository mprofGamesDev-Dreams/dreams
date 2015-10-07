using UnityEngine;
using System.Collections;

public class OnEnemyDeathTrigger : MonoBehaviour {

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
			myAudioEvent.TriggerEvent = true;
			Destroy(this);
		}
	}
}
