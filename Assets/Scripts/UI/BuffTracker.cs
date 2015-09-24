using UnityEngine;
using System.Collections;

public class BuffTracker : MonoBehaviour {

	[SerializeField] private GameObject imagiBuff;
	[SerializeField] private GameObject player;

	private PlayerStats playerStats;

	// Use this for initialization
	void Start () {
		playerStats = player.GetComponent<PlayerStats> ();
	}
	
	// Update is called once per frame
	void Update () {
		if ( playerStats.Buffed == true && imagiBuff.activeInHierarchy == false) 
		{
			imagiBuff.SetActive(true);
		} 
		else if (playerStats.Buffed == false && imagiBuff.activeInHierarchy == true) 
		{
			imagiBuff.SetActive(false);
		}
	}
}
