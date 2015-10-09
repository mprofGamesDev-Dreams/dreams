using UnityEngine;
using System.Collections;

public class VoidPickup : MonoBehaviour {

    [SerializeField] private float resource = 50.0f;

	private OnCallPlayEventAudio audioEvent;

	private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    private GameObject player;
	
	// Use this for initialization
	void Start () {
		audioEvent = GetComponent<OnCallPlayEventAudio>();
	    audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player = col.gameObject;            
            
            if (player.GetComponent<InputHandler>().isInteract() && resource > 0)
            {
                Invoke("PickUp", audioClip.length);
                audioSource.PlayOneShot(audioClip);
            }
            
        }
    }

    void PickUp()
    {
        player.gameObject.GetComponent<PlayerStats>().ModifyVoid(resource);
		if(audioEvent != null && audioEvent.enabled)	
			audioEvent.TriggerEvent = true;
        Destroy(gameObject);
    }
}
