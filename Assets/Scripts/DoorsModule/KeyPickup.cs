using UnityEngine;
using System.Collections;

/*
 * Script to handle picking up the keys
 * On trigger collision, it will flag that the key has been collected in the player keychain
 * 
 * ATTACHMENT : Attach to the key object in game and set the key name.
 * Make sure name matches up with a name in the keychain script attached to the player.
 */

public class KeyPickup : MonoBehaviour
{
	public string KeyName = "";

	void Start ()
	{
	
	}
	
	void OnTriggerEnter(Collider col)
	{
		Debug.Log(col.gameObject.name);

		if( col.gameObject.name == "Player")
		{
			// Get access to the key chain
			KeyChain PlayerKeys = GameObject.Find("Player").GetComponent<KeyChain>();

			// Collect the keys
			PlayerKeys.CollectKey(KeyName);

			// Destroy this object
			GameObject.Destroy(this.gameObject);
		}
	}
}
