using UnityEngine;
using System.Collections;

public class UnlockDoorTrigger : MonoBehaviour
{
	// Door to unlock
	public GameObject DoorToUnlock;

	// Access to door lock script
	private DoorLock LockScript;

	void Start ()
	{
		// Get the door unlock script
		if(DoorToUnlock)
			LockScript = DoorToUnlock.GetComponent<DoorLock>();
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.name == "Player")
		{
			// Unlock the door
			LockScript.UnlockDoor();
		}
	}
}
