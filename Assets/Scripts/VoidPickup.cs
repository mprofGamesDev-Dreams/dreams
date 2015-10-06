using UnityEngine;
using System.Collections;

public class VoidPickup : MonoBehaviour {

    [SerializeField] private float resource = 50.0f;

    private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    private GameObject player;
	// Use this for initialization
	void Start () {
	    audioSource = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider col)
    {
        player = col.gameObject;
		if (player.CompareTag ("Player")) 
        {
			if (player.GetComponent<InputHandler> ().isInteract () && resource > 0)
            {
                Invoke("PickUp", audioClip.length);
                audioSource.PlayOneShot(audioClip);
			}
		}
    }

    void PickUp()
    {
        player.gameObject.GetComponent<PlayerStats>().ModifyVoid(resource);
        Destroy(gameObject);
    }
}
