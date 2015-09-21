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
	public float OpenDistance = 3;
	public float OpenSpeed = 2.5f;

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
		else
		{
			Debug.Log("No access to player");
		}

		OpenDoorPosition = transform.position + (transform.up*-5);
	}

	void Update ()
	{
		// Check if the player has the key required to unlock
		if(PlayerKeyChain)
			PlayerHasKey = PlayerKeyChain.CheckKey(KeyName);

		// If the player has the key
		if(PlayerHasKey || !DoorLocked)
		{
			// Check the distance to the door
			float Distance = Vector3.Distance(transform.position, Player.transform.position);

			// If the player is close enough, move the door
			if( Distance <= OpenDistance)
			{
				IsOpeningDoor = true;
			}
		}

        if( IsOpeningDoor )
		{
			transform.position = Vector3.MoveTowards(transform.position, OpenDoorPosition, OpenSpeed * Time.deltaTime);

			if(transform.position.Equals (OpenDoorPosition))
				IsOpeningDoor = false;
		}
	}

	public void UnlockDoor()
	{
		DoorLocked = false;
	}
}
