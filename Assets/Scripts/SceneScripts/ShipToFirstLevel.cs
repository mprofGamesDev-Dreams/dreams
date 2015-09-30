using UnityEngine;
using System.Collections;

public class ShipToFirstLevel : MonoBehaviour 
{
	[SerializeField] private AudioClip[] audioClips;
	private int audioIndex = 0;

	private GameObject playerObject;
	private InputHandler playerInputManager;

	private AudioSource narrator;

	[SerializeField]private LoadSceneOnTrigger trigger;


	private bool terminate;
	private float startTime;
	private void Start()
	{
		playerObject = GameObject.FindGameObjectWithTag("Player");
		playerInputManager = playerObject.GetComponent<InputHandler>();

		playerInputManager.ControllerConstraints = EControlConstraints.DisableAllExceptCamera;

		narrator = playerObject.AddComponent<AudioSource>();
		narrator.clip = audioClips[audioIndex];
		narrator.Play();
		startTime = Time.time;
	}

	private void Update()
	{

		if(narrator.clip == null)
			return;

		if( Time.time > (startTime + narrator.clip.length) && !terminate)
		{
			audioIndex++;
			if(audioIndex == 1)
			{
				playerInputManager.ControllerConstraints = EControlConstraints.EnableAllExceptPowers;
				narrator.clip = audioClips[audioIndex];
				narrator.Play();
			}

			if(audioIndex == 2)
			{
				narrator.clip = audioClips[audioIndex];
				narrator.Play ();

				playerInputManager.ControllerConstraints = EControlConstraints.EnableAll;
			}

			if(audioIndex == 3)
			{
				trigger.canTeleport = true;
			}
			startTime = Time.time;
		}
	}
}
