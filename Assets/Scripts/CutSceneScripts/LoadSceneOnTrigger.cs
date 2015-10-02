using UnityEngine;
using System.Collections;

public class LoadSceneOnTrigger : MonoBehaviour 
{
	public bool canTeleport;
	private bool activateFadeOut;
	public string levelToLoad;
	public WhiteFlash flashController;

	public Light lightSource;

	private void Update()
	{
		if(activateFadeOut)
		{
			flashController.FadeToWhite();
			StartCoroutine(WaitToLoadScene(flashController.fadeOutSpeed));
		}
	}

	private void OnTriggerEnter(Collider obj)
	{
		if(obj.gameObject.CompareTag("Player") && canTeleport)
		{
			activateFadeOut = true;
		}

		if( obj.gameObject.CompareTag("Bullet") && obj.GetComponent<Bullet>().BulletType == ActivePower.Logio )
		{
			lightSource.color = Color.green;
			canTeleport = true;
		}
	}

	private IEnumerator WaitToLoadScene(float t)
	{
		yield return new WaitForSeconds(t);
		Application.LoadLevel(levelToLoad);
	}

}
