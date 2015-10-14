using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {
	private Transform mainCameraTransform;
	// Use this for initialization
	void Start () {
		// Make Sure main Camera is The First Child
		mainCameraTransform = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).transform;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.LookAt (mainCameraTransform.position, Vector3.up);
	}
}
