using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {
	private Transform mainCameraTransform;
	// Use this for initialization
	void Start () {
		mainCameraTransform = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.LookAt (mainCameraTransform.position, Vector3.up);
	}
}
