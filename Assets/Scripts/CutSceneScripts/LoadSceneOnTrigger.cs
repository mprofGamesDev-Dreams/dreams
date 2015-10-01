using UnityEngine;
using System.Collections;

public class LoadSceneOnTrigger : MonoBehaviour 
{
	public bool canTeleport;
	private bool activateFadeOut;
	public string levelToLoad;
	public WhiteFlash flashController;

	public Light light;
	private void Update()
	{
		if(activateFadeOut)
		{
			flashController.FadeToWhite();
			StartCoroutine(WaitToLoadScene(flashController.fadeOutSpeed));
		}
	}

	private void OnTriggerStay(Collider obj)
	{
		if(obj.gameObject.CompareTag("Player") && canTeleport)
		{
			activateFadeOut = true;
		}

		if(obj.gameObject.CompareTag("Bullet") && obj.GetComponent<Bullet>().BulletType == ActivePower.Logio )
		{
			Debug.Log("hit by bullet");
			canTeleport = true;
			light.color = Color.green;
		}
	}

	private IEnumerator WaitToLoadScene(float t)
	{
		yield return new WaitForSeconds(t);
		Application.LoadLevel(levelToLoad);
	}

}
