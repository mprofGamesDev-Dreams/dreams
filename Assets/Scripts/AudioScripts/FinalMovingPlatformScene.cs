using UnityEngine;
using System.Collections;

public class FinalMovingPlatformScene : MonoBehaviour 
{
	[SerializeField] private Transform dreamer;
	[SerializeField] private float transitionTime;

	private InputHandler playerInput;
	//private Transform cameraTransform;

	private bool triggerEvent = true; public bool TriggerEvent { set { triggerEvent = true; } }

	OnTriggerPlay audioEvent;

	private void Start () 
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");

		playerInput = player.GetComponent<InputHandler>();
		//cameraTransform = player.GetComponent<Transform>();
		//cameraTransform = player.GetComponent<Transform>().GetChild(0).GetComponent<Transform>();
		audioEvent = GetComponent<OnTriggerPlay>();
	}
	
	private void Update () 
	{
		/*
		if( audioEvent.CurrentState != EAudioState.isFinished && audioEvent.CurrentState != EAudioState.isWaiting )
		{
			cameraTransform.rotation = Quaternion.Euler( new Vector3( cameraTransform.rotation.eulerAngles.x, cameraTransform.rotation.eulerAngles.y + rot, cameraTransform.rotation.eulerAngles.z  ) );
			//cameraTransform.forward = ( dreamer.position - cameraTransform.position ).normalized;
		}*/

		if( audioEvent.CurrentState != EAudioState.isWaiting && triggerEvent )
		{
			playerInput.ControllerConstraints = EControlConstraints.DisableAll;


			StartCoroutine( UnlockPlayer() );

			triggerEvent = false;
		}

		if( audioEvent.CurrentState != EAudioState.isFinished )
		{
			playerInput.ControllerConstraints = EControlConstraints.EnableAll;
		}
	}

	private IEnumerator UnlockPlayer()
	{
		yield return new WaitForSeconds(transitionTime);

	}
}
