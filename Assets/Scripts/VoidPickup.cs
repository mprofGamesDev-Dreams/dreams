using UnityEngine;
using System.Collections;

public class VoidPickup : MonoBehaviour {

    [SerializeField] private float resource = 50.0f;
	private OnCallPlayEventAudio audioEvent;

	// Use this for initialization
	void Start () {
		audioEvent = GetComponent<OnCallPlayEventAudio>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider player)
    {
		if (player.CompareTag ("Player")) {
			if (player.GetComponent<InputHandler> ().isInteract () && resource > 0) {
				player.gameObject.GetComponent<PlayerStats> ().ModifyVoid (resource);
				audioEvent.TriggerEvent = true;
				Destroy (gameObject);
			}
		}
    }
}
