using UnityEngine;
using System.Collections;

public class BuffTracker : MonoBehaviour {

	[SerializeField] private GameObject imagiBuff;
	[SerializeField] private GameObject enemyBuff;
	[SerializeField] private GameObject player;

	private PlayerStats playerStats;

	// Use this for initialization
	void Start () {
		playerStats = player.GetComponent<PlayerStats> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateImagiBuff();
		UpdateEnemyBuff();
	}

	private void UpdateImagiBuff()
	{
		if ( playerStats.Buffed == true && imagiBuff.activeInHierarchy == false) 
		{
			imagiBuff.SetActive(true);
		} 
		else if (playerStats.Buffed == false && imagiBuff.activeInHierarchy == true) 
		{
			imagiBuff.SetActive(false);
		}
	}

	private void UpdateEnemyBuff()
	{
		if ( playerStats.Debuffed == true && enemyBuff.activeInHierarchy == false) 
		{
			enemyBuff.SetActive(true);
		} 
		else if (playerStats.Debuffed == false && enemyBuff.activeInHierarchy == true) 
		{
			enemyBuff.SetActive(false);
		}
	}
}
