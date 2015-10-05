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

		playerObject.GetComponent<AbilityBehaviours>().CurrentPower = ActivePower.Logio;

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

			if(audioIndex == 3)
			{
				terminate = true;
			}
			startTime = Time.time;
		}
	}

	public void PlayClip(int i) // played after shooting
	{
		narrator.clip = audioClips[i];
		narrator.Play();
		startTime = Time.time;

        StartCoroutine(WaitFor(audioClips[i].length));
	}

    private IEnumerator WaitFor(float time)
    {
        trigger.GetComponent<BoxCollider>().isTrigger = false;
        Debug.Log("Coroutine started");
        yield return new WaitForSeconds(time);

        trigger.GetComponent<BoxCollider>().isTrigger = true;
        Debug.Log("Coroutine Waited");
    }
}

