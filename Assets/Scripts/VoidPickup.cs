using UnityEngine;
using System.Collections;

public class VoidPickup : MonoBehaviour {

    [SerializeField] private float resource = 50.0f;

	private OnCallPlayEventAudio audioEvent;

	private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    private GameObject player;

	private bool isUsed = false;

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
        if (col.gameObject.CompareTag("Player") && !isUsed)
        {
            player = col.gameObject;            
            
            if (player.GetComponent<InputHandler>().isInteract() && resource > 0)
            {
				isUsed = !isUsed;
                player.GetComponent<InputHandler>().PlayInteract();
                StartCoroutine("wait");
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

    IEnumerator wait()
    {
        yield return new WaitForSeconds(2.0f);
    }
}
