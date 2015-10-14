/*
 * Script for fight on the final platform
 * All first enemies must be provided in editor
 * All floating islands must be provided in their appropriate field
 * The sequence is as follows:
 * First enemies -> audio
 * Second wave
 * Third Wave
 * Fourth Wave
 * Audio
 * Load final cutscene
 * 
 * Must be attached to an object with the Enemy CounterSingleton class
 * 
 * GC
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FinalFightScript : MonoBehaviour {
	[SerializeField] private List<GameObject> firstWaveEnemies;
	[SerializeField] private List<GameObject> secondWaveEnemies;
	[SerializeField] private List<GameObject> thirdWaveEnemies;
	[SerializeField] private List<GameObject> fourthWaveEnemies;
	//[SerializeField] private GameObject[]  thirdWaveEnemies;
	[SerializeField] private GameObject[]  secondWaveIslands;
	[SerializeField] private GameObject[]  thirdWaveIslands;
	[SerializeField] private GameObject[]  fourthWaveIslands;
	[SerializeField] private AudioClip clip1;
	[SerializeField] private AudioClip clip2;
	[SerializeField] private AudioSource narator;

	[SerializeField] private GameObject flash;

	private int stage =1;
	private bool firstTrigger = false;
	private bool secondTrigger = false;
	private bool thirdTrigger = false;
	private bool fourthTrigger = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		switch (stage) 
		{
		case 1:
			firstWaveEnemies.RemoveAll(item => item == null);
			if ( firstWaveEnemies.Count == 0 && firstTrigger == false)
			{
				firstTrigger = true;
				//FIRST TRANSITION
				//Debug.Log("first transition");
				//int test = 0;
				StartCoroutine("FirstTransition");
			}
			break;
		case 2:
			secondWaveEnemies.RemoveAll(item => item == null);
			if ( secondWaveEnemies.Count == 0 && secondTrigger == false)
			{
				//TRANSITION
				secondTrigger = true;
				StartCoroutine("SecondTransition");
			}
			break;
		case 3:
			thirdWaveEnemies.RemoveAll(item => item == null);
			if ( thirdWaveEnemies.Count == 0 && thirdTrigger == false)
			{
				//TRANSITION
				thirdTrigger = true;
				StartCoroutine("ThirdTransition");
			}
			break;
		case 4:
			fourthWaveEnemies.RemoveAll(item => item == null);
			if ( fourthWaveEnemies.Count == 0 && fourthTrigger == false)
			{
				//TRANSITION
				fourthTrigger = true;
				StartCoroutine("FourthTransition");
			}
			break;
		}
	}

	private IEnumerator FirstTransition()
	{
		//ALL ENEMIES ARE DEAD
		//PLAY: "NOW! USE YOUR EVO PARTICLES ON THE DREAMER!" CLIP1
		narator.clip = clip1;
		narator.Play();
		//WAIT LENGTH +1
		yield return new WaitForSeconds (clip1.length);
		//FLASH
		flash.GetComponent<WhiteFlash> ().RequestFlash();
		yield return new WaitForSeconds (2);
		//flash.GetComponent<WhiteFlash> ().FadeToClear ();
		//PLAY: "THE DREAMER'S RESISTING - THEY DON'T WANT TO LEAVE" CLIP2
		narator.clip = clip2;
		narator.Play();
		//WAIT LENGTH
		yield return new WaitForSeconds (clip2.length);
		//TRIGGER SECOND WAVE ISLANDS
		foreach (GameObject island in secondWaveIslands) {
			island.GetComponent<TransformGeometry>().Trigger();
			secondWaveEnemies.Add(island.GetComponent<SpawnEnemyOnPlatform>().Enemy);
		}
		//CHANGE STAGE = 2
		stage = 2;
		yield return null;

	}
	private IEnumerator SecondTransition()
	{
		//ALL ENEMIES ARE DEAD
		//TRIGGER THIRD WAVE ISLANDS
		foreach (GameObject island in thirdWaveIslands) {
			island.GetComponent<TransformGeometry>().Trigger();
			thirdWaveEnemies.Add(island.GetComponent<SpawnEnemyOnPlatform>().Enemy);
		}
		//CHANGE STATE
		stage = 3;
		yield return null;
	}

	private IEnumerator ThirdTransition()
	{
		//ALL ENEMIES ARE DEAD
		//TRIGGER THIRD WAVE ISLANDS
		foreach (GameObject island in fourthWaveIslands) {
			island.GetComponent<TransformGeometry>().Trigger();
			fourthWaveEnemies.Add(island.GetComponent<SpawnEnemyOnPlatform>().Enemy);
		}
		//CHANGE STATE
		stage = 4;
		yield return null;
	}

	private IEnumerator FourthTransition()
	{
		//ALL ENEMIES ARE DEAD
		//PLAY: "NOW! USE YOUR EVO PARTICLES ON THE DREAMER!" CLIP 1
		narator.clip = clip1;
		narator.Play();
		//WAIT LENGTH +1
		yield return new WaitForSeconds (clip1.length);

		//FLASH
		//flash.GetComponent<WhiteFlash> ().RequestFlash();
		//yield return new WaitForSeconds (2);
		//TRIGGER END CUTSCENE
		gameObject.GetComponent<EnemyCounterSingleton> ().StartCutscene ();
		yield return null;
	}
}
