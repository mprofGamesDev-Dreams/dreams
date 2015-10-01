using UnityEngine;
using System.Collections;

public class CheckpointUpdate : MonoBehaviour
{
	[SerializeField] private CheckpointManager manager;
	 
	void OnTriggerEnter()
	{
		if(!manager)
			return;

		manager.SetLastPos (transform.position);
		Destroy (gameObject);
		Destroy (this);
	}
}