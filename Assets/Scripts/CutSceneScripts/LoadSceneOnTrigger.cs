using UnityEngine;
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

	private void OnTriggerEnter(Collider obj)
	{
		if(obj.gameObject.CompareTag("Player") && canTeleport)
		{
			activateFadeOut = true;
		}

		if( obj.gameObject.CompareTag("Bullet") && obj.GetComponent<Bullet>().BulletType == ActivePower.Logio  && !canTeleport)
		{
			sceneController.PlayClip(2);
			lightSource.color = Color.green;
		}
	}

	private IEnumerator WaitToLoadScene(float t)
	{
		yield return new WaitForSeconds(t);
		Application.LoadLevel(levelToLoad);
	}

}
