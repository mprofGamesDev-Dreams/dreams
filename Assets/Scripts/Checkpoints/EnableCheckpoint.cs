using UnityEngine;
using System.Collections;

public class EnableCheckpoint : MonoBehaviour {

	[SerializeField] private GameObject checkpoint;

	// Use this for initialization
	void Start () {
		checkpoint.SetActive (false);
	}

	void OnTriggerEnter()
	{
		if(checkpoint!=null) checkpoint.SetActive (true);
	}
}
