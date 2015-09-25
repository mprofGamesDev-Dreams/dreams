using UnityEngine;
using System.Collections;

public class VoidPickup : MonoBehaviour {

    [SerializeField] private float resource = 50.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider player)
    {
        if (player.GetComponent<InputHandler>().isInteract() && resource >0 )
        {
            player.gameObject.GetComponent<PlayerStats>().ModifyVoid(resource);
            Destroy(gameObject);
        }
    }
}
