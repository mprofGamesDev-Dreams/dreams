using UnityEngine;
using System.Collections;

public class CallOnModelSwitchTrigger : MonoBehaviour, IDestroyAudioEvent
{
	private ModelSwitch modelSwitch;
	[SerializeField] private AudioClip clip;

	private void Start()
	{
		modelSwitch = GetComponent<ModelSwitch>();
	}

	private void Update()
	{
		if(modelSwitch.IsTriggered)
		{
			NarratorController.NarratorInstance.PlayNewClip(clip, gameObject.GetInstanceID(), this);
		}
	}

	public void DestroyAudioEvent()
	{
		Destroy(this);
	}
}
