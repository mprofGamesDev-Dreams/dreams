using UnityEngine;
using System.Collections;

//triggers Evo partigle guage. place on any object and provide parent of resource bar.

public class Triggerparticles : MonoBehaviour {

	public bool BossTrigger;
	public GameObject bar;
	// Use this for initialization
	void Start () {
		BossTrigger = false;
		bar.active = false;
	}
	
	// Update is called once per frame
	void Update () {

		//DEBUG CODE
		if (BossTrigger == true && bar.active == false) 
		{
			bar.active = true;
		}
		if (BossTrigger == false && bar.active == true) 
		{
			bar.active = false;
		}
	
	}

	public void TriggerBoss()
	{
		BossTrigger = true;
		bar.active = true;
	}
	public void EndBoss()
	{
		BossTrigger = false;
		bar.active = false;
	}
}
