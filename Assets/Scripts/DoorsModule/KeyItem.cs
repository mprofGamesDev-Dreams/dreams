using UnityEngine;
using System.Collections;

/*
 * Just a class for storing key details
 */

[System.Serializable]
public class KeyItem
{
	public string Name;
	public bool Collected;

	// Checks the key name against a passed string
	public bool CheckName(string Other)
	{
		return Name.Equals(Other);
	}
	
	// Return whether key is collected
	public bool IsCollected()
	{
		return Collected;
	}
	
	// Flag the key is collected
	public void SetCollected()
	{
		Collected = true;
	}
}