using UnityEngine;
using System.Collections;

public class ShrinkToOblivion : MonoBehaviour 
{
	private Transform myTransform;

	public bool trigger;

	public float shrinkSpeed;

	// Use this for initialization
	void Start () 
	{
		myTransform = GetComponent<Transform>();
		shrinkSpeed *= Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( myTransform.localScale != Vector3.zero && trigger )
		{
			myTransform.localScale = new Vector3(Mathf.Clamp(myTransform.localScale.x - shrinkSpeed, 0, Mathf.Infinity), Mathf.Clamp(myTransform.localScale.y - shrinkSpeed, 0, Mathf.Infinity), Mathf.Clamp(myTransform.localScale.z - shrinkSpeed, 0, Mathf.Infinity));
		}
	}


}
