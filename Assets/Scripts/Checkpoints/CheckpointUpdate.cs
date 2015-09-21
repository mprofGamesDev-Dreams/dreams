using UnityEngine;
using System.Collections;

public class CheckpointUpdate : MonoBehaviour {
	
	[SerializeField] private CheckpointManager manager;
	[SerializeField] private GameObject checkpoint;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(){
		manager.SetLastPos (transform.position);
		Destroy (checkpoint);
		Destroy (this);
	}
	
}
