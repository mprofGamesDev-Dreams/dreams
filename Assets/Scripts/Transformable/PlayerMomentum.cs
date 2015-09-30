using UnityEngine;
using System.Collections;

// Script to ensure player moves with moving platforms
// must be added to a trigger that is a child of the player object.
public class PlayerMomentum : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log(gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().velocity);
	}

	void OnTriggerStay(Collider collider)
	{
		//GameObject collider = collision.gameObject;

		if (collider.tag == "Platform") 
		{
            //Debug.Log("collided with " + collider.name);		
			gameObject.transform.parent.parent = collider.transform;
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.tag == "Platform") 
		{
			gameObject.transform.parent.parent=null;
		}
	}
}
