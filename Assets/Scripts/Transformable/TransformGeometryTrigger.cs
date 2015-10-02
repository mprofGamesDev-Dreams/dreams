using UnityEngine;
using System.Collections;

public class TransformGeometryTrigger : MonoBehaviour {

    
    [SerializeField] private GameObject transformableObject;


    void OnTriggerEnter(Collider colliderIn)
    {
		if( colliderIn.gameObject.CompareTag("Player") )
	        transformableObject.GetComponent<TransformGeometry>().SendMessage("Trigger");
        //Destroy(gameObject);
    }
}
