using UnityEngine;
using System.Collections;

public class LoadSceneOnTrigger : MonoBehaviour 
{
	public bool canTeleport;
	private bool activateFadeOut;
	public string levelToLoad;
	public WhiteFlash flashController;
	public TriggerSky skyDoor;
	public Light lightSource;

	[SerializeField]private ShipToFirstLevel sceneController;
	
	private void Update()
	{
		if(activateFadeOut)
		{
			flashController.RequestFlash();
            StartCoroutine(WaitToLoadScene(GetComponent<AudioSource>().clip.length));
		}
	}

	private void OnCollisionEnter(Collision obj)
	{
		if (obj.gameObject.CompareTag("Bullet"))
		{
			if (obj.gameObject.GetComponent<Bullet>().BulletType == ActivePower.Logio)
			{
				if (!canTeleport)
				{
					sceneController.PlayClip(2);
					lightSource.color = Color.green;
					canTeleport = true;
					skyDoor.isTriggered = true;
				}
			}
		}
		/*
		else if(obj.gameObject.CompareTag("Player") && canTeleport)
		{
			activateFadeOut = true;
			GetComponent<AudioSource>().Play();
			obj.gameObject.GetComponent<InputHandler>().ControllerConstraints = EControlConstraints.DisableAll;
		}*/
	}

	private void OnTriggerEnter(Collider obj)
	{
		if(obj.gameObject.CompareTag("Player") && canTeleport)
		{
			activateFadeOut = true;
            GetComponent<AudioSource>().Play();
			obj.gameObject.GetComponent<InputHandler>().ControllerConstraints = EControlConstraints.DisableAll;
		}
	}

	private IEnumerator WaitToLoadScene(float t)
	{
		yield return new WaitForSeconds(t);
		Application.LoadLevel(levelToLoad);
	}
}