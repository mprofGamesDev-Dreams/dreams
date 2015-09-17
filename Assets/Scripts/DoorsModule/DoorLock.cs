using UnityEngine;
using System.Collections;

/*
 * Script to open a door. Automatically opens the door when the player is near. 
 * 
 * ATTACHMENT : Attach this script to the door object and specify the name of the key the player needs.
 *
 */

public class DoorLock : MonoBehaviour
{
	// Public config
	public string KeyName = "";
	public bool DoorLocked = true;

	// Object references
	private GameObject Player;
	private KeyChain PlayerKeyChain;

	// Bools
	public bool PlayerHasKey = false;
	private bool IsOpeningDoor = false;

	// Set the position of an open door
	private Vector3 OpenDoorPosition;

	void Start ()
	{
		// Find the player game object
		Player = (GameObject)GameObject.Find ("Player");

		// Find the keychain component
		if (Player)
		{
			PlayerKeyChain = Player.GetComponent<KeyChain>();
		}

		OpenDoorPosition = transform.position + (transform.up*-5);
	}

	void Update ()
	{
		// Check if the player has the key required to unlock
		PlayerHasKey = PlayerKeyChain.CheckKey(KeyName);

		// If the player has the key
		if(PlayerHasKey)
		{
			// Check the distance to the door
			float Distance = Vector3.Distance(transform.position, Player.transform.position);

			// If the player is close enough, move the door
			if( Distance < 3)
			{
				IsOpeningDoor = true;
			}
		}

		// THIS WILL NEED TO BE REPLACE WITH SWINING DOOR WHEN ARTISTS DONE
        if( IsOpeningDoor )
		{
			transform.position += transform.up * -Time.deltaTime;

			if(transform.position.Equals (OpenDoorPosition))
				IsOpeningDoor = false;
		}
	}
}
