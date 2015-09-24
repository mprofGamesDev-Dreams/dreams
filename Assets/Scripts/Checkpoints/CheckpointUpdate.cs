using UnityEngine;
using System.Collections;

public class CheckpointUpdate : MonoBehaviour {
	
	[SerializeField] private CheckpointManager manager;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	 
	void OnTriggerEnter(){
		manager.SetLastPos (transform.position);
		Destroy (gameObject);
		Destroy (this);
	}
	
}
