using UnityEngine;
using System.Collections;

//triggers Evo partigle guage. place on any object and provide parent of resource bar.

public class Triggerparticles : MonoBehaviour {

	public bool BossTrigger;
	public GameObject bar;
	// Use this for initialization
	void Start () {
		BossTrigger = false;
		bar.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

		//DEBUG CODE
		if (BossTrigger == true && bar.activeInHierarchy == false) 
		{
			bar.SetActive(true);
		}
		if (BossTrigger == false && bar.activeInHierarchy == true) 
		{
			bar.SetActive(false);
		}
	
	}

	public void TriggerBoss()
	{
		BossTrigger = true;
		bar.SetActive(true);
	}
	public void EndBoss()
	{
		BossTrigger = false;
		bar.SetActive(false);
	}
}
