using UnityEngine;
using System.Collections;

public class BuffTracker : MonoBehaviour {

	[SerializeField] private GameObject imagiBuff;
	[SerializeField] private GameObject player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent<PlayerStats> ().Buffed == true && imagiBuff.active == false) 
		{
			imagiBuff.active = true;
		} 
		else if (player.GetComponent<PlayerStats> ().Buffed == false && imagiBuff.active == true) 
		{
			imagiBuff.active = false;
		}
	}
}
