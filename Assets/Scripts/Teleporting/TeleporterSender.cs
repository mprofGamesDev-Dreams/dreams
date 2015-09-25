using UnityEngine;
using System.Collections;

public class TeleporterSender : MonoBehaviour
{
	
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject sender;
	[SerializeField] private GameObject receiver;
	[SerializeField] private WhiteFlash flash;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter (Collider collision)
	{
        if (collision.gameObject.GetComponent<PlayerStats>())
        {
            flash.RequestFlash();
            StartCoroutine(Teleport());
        }
	}

	IEnumerator Teleport(){
		//Delay teleporting to allow screen to fade to white
		yield return new WaitForSeconds (1.0f);
		// Move player
		player.transform.position = receiver.transform.position;
		// Destroy teleport objects as no longer required
		Destroy (sender);
		Destroy (this);
		Destroy (receiver);
	}
}
