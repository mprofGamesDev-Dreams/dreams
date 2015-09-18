using UnityEngine;
using System.Collections;

/*
 *  Script to handle the player keys
 *  Keys can be added via in the inspector. Make sure these all match up.
 * 
 *  ATTACHMENT: Attach this script to the player
 */

public class KeyChain : MonoBehaviour
{
    // Array to store the keys
    // Set these in inspector
	public KeyItem[] Keys;

	// Check if the player has the key
	public bool CheckKey(string name)
	{
		for(int i = 0; i < Keys.Length; i++)
		{
			if( Keys[i].CheckName(name))
			{
				return Keys[i].IsCollected();
			}
		}

		return false;
	}

	// Put the key in the key chain
	public void CollectKey(string name)
	{
		for(int i = 0; i < Keys.Length; i++)
		{
			if( Keys[i].CheckName(name))
			{
				Keys[i].SetCollected();
			}
        }
    }
}
