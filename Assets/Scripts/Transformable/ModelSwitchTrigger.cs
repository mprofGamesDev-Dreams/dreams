using UnityEngine;
using System.Collections;

/*ModelSwitchTrigger
 * 
 * This Script should be attached to a gameobject with a trigger collider
 * And the the game object to be added will dissapear and spawn an object in its place when this object is entered
 */

public class ModelSwitchTrigger : MonoBehaviour {

    [SerializeField] private GameObject transformableObject;
	
    void OnTriggerEnter(Collider colliderIn)
    {
		if (colliderIn.gameObject.CompareTag("Player"))
		{
	        transformableObject.GetComponent<ModelSwitch>().SendMessage("Trigger");
	        Destroy(gameObject);
		}
	}
}
