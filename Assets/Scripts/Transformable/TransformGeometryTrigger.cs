using UnityEngine;
using System.Collections;

public class TransformGeometryTrigger : MonoBehaviour {

    
    [SerializeField] private GameObject transformableObject;


    void OnTriggerEnter(Collider colliderIn)
    {
        transformableObject.GetComponent<TransformGeometry>().SendMessage("Trigger");
        //Destroy(gameObject);
    }
}
