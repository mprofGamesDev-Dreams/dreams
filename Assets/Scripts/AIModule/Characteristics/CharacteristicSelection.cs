using UnityEngine;
using System;

[Flags] public enum AICharacteristics
{
	Normal,
	SplitOnHit,
	Scale,
	FastEnemies
}

public class CharacteristicSelection : MonoBehaviour 
{
	public AICharacteristics myCharacteristics;

	private void Start () 
	{
		if ((myCharacteristics & AICharacteristics.Normal) == 0)
			Debug.Log ("Normal");

		if ((myCharacteristics & AICharacteristics.SplitOnHit) == 0)
			Debug.Log ("Split");

		if ((myCharacteristics & AICharacteristics.Scale) == 0)
			Debug.Log ("Scale");

		if ((myCharacteristics & AICharacteristics.FastEnemies) == 0)
			Debug.Log ("Fast");
	}
	
	private void Update () 
	{
		
	}
}
