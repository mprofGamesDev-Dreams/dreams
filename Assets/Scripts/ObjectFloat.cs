using UnityEngine;
using System.Collections;

public class ObjectFloat : MonoBehaviour 
{
	[SerializeField] private float range;
	[SerializeField] private float speed;

	[SerializeField]private Vector3 floatDir;
	private Transform myTransform;

	// Use this for initialization
	void Start () 
	{
		myTransform = GetComponent<Transform>();
		floatDir = new Vector3( 
		                       	 floatDir.x == 0 ? 0 : Mathf.Sign(floatDir.x)
		                       , floatDir.y == 0 ? 0 : Mathf.Sign(floatDir.y)
		                       , floatDir.z == 0 ? 0 : Mathf.Sign(floatDir.z) );
	}
	
	// Update is called once per frame
	void Update () 
	{
		float sin = Mathf.Sin(Time.time) * speed;
		myTransform.position += new Vector3( floatDir.x * sin * range, floatDir.y * sin * range, floatDir.z * sin * range );
		Debug.Log(new Vector3( floatDir.x * sin * range, floatDir.y * sin * range, floatDir.z * sin * range ));
	}
}
