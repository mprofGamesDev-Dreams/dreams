using UnityEngine;
using System.Collections;

public class SpawnAnimation : MonoBehaviour {

	private Transform myTransform;

	//Scaling variables
	private Vector3 targetScale;
	private float growSpeed = 0.5f;

	//Position variables
	private Vector3 pos;
	private float frequency = 1.0f;
	private float magnitude = 0.5f;
	private float time;

	private bool animationDone = false;

	// Use this for initialization
	void Start () {
		myTransform = GetComponent<Transform> ();
		targetScale = myTransform.localScale;
		myTransform.localScale = new Vector3 (0, 0, 0);

		pos = myTransform.position;
	}
	
	// Update is called once per frame
	void Update () {

		//Grows to it's target size upon startup
		if (myTransform.localScale != targetScale) {
			myTransform.localScale = new Vector3 (
				Mathf.Clamp (myTransform.localScale.x + (growSpeed * Time.deltaTime), 0, targetScale.x),
				Mathf.Clamp (myTransform.localScale.y + (growSpeed * Time.deltaTime), 0, targetScale.y),
				Mathf.Clamp (myTransform.localScale.z + (growSpeed * Time.deltaTime), 0, targetScale.z));
			growSpeed += (1.0f * Time.deltaTime);
		}

		if (animationDone)
			return;

		//Position update; bounce for half of a sin wave
		time += Time.deltaTime;
		if (time * frequency <= 0.5f) {
			myTransform.position = pos + (Mathf.Sin (2 * Mathf.PI * time * frequency) * magnitude * transform.up);
			//time = Time.time * frequency;
		} else {
			myTransform.position = pos + (Mathf.Sin (2 * Mathf.PI * 0.5f) * magnitude * transform.up);
			animationDone = true;
		}
	}
}
