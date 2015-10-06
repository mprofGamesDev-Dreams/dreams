﻿using UnityEngine;
using System.Collections;

public class LoadSceneOnTrigger : MonoBehaviour 
{
	public bool canTeleport;
	private bool activateFadeOut;
	public string levelToLoad;
	public WhiteFlash flashController;

	public Light lightSource;

	public AudioClip clip;
	public AudioSource source;

	[SerializeField]private ShipToFirstLevel sceneController;

	private void Start()
	{
		source = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
	}

	private void Update()
	{


		if(activateFadeOut)
		{
			flashController.FadeToWhite();
			StartCoroutine(WaitToLoadScene(flashController.fadeOutSpeed));
		}
	}

	private void OnCollisionEnter(Collision obj)
	{
		if (obj.gameObject.CompareTag("Bullet"))
		{
			//Debug.Log("IT WAS A BULLET");
			
			if (obj.gameObject.GetComponent<Bullet>().BulletType == ActivePower.Logio)
			{
				//Debug.Log("AND LOGIO");
				if (!canTeleport)
				{
					//Debug.Log("AND CANT TELEPORT");
					sceneController.PlayClip(2);
					lightSource.color = Color.green;
					canTeleport = true;
				}
			}
		}
	}

	private void OnTriggerEnter(Collider obj)
	{
		if(obj.gameObject.CompareTag("Player") && canTeleport)
		{
			activateFadeOut = true;
		}

        //Debug.Log(obj.gameObject.tag +"hit THE DOOR");
        
		//if( obj.gameObject.CompareTag("Bullet") && obj.GetComponent<Bullet>().BulletType == ActivePower.Logio  && !canTeleport)

	}

	private IEnumerator WaitToLoadScene(float t)
	{
		yield return new WaitForSeconds(t);
		Application.LoadLevel(levelToLoad);
	}

}
