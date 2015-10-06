using UnityEngine;
using System.Collections;

public class CheckpointUpdate : MonoBehaviour
{
	[SerializeField] private CheckpointManager manager;
	 
	void OnTriggerEnter()
	{
		if(!manager)
			return;


		manager.SetLastPos (GetComponentInParent<Transform>().position);
		Destroy (gameObject);
		Destroy (this);
	}
}