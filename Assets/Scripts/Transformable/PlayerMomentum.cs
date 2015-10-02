using UnityEngine;
using System.Collections;

// Script to ensure player moves with moving platforms
// must be added to a trigger that is a child of the player object.
public class PlayerMomentum : MonoBehaviour {

	private Vector3 playerScale;
	// Use this for initialization
	void Start () {
		playerScale = gameObject.transform.parent.localScale;
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
			gameObject.transform.parent.localScale = new Vector3(playerScale.x/collider.transform.localScale.x,
			                                                     playerScale.y/collider.transform.localScale.y,
			                                                     playerScale.z/collider.transform.localScale.z);
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
