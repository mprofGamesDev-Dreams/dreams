using UnityEngine;
using System.Collections;

public class TriggerSky : MonoBehaviour {

	public bool isTriggered = false;
	public GameObject image;
	[SerializeField] private float fadeInTime = 2.0f;
	[SerializeField] private float fadeInTimer = 0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isTriggered) {
			StartCoroutine("Fade");
		}
	}

	IEnumerator Fade()
	{
		isTriggered = false;
		Renderer SkyTexture = gameObject.GetComponent<Renderer> ();
		Color targetColor = SkyTexture.material.color;
		targetColor.a = 0.0f;
		//SkyTexture.SetColor(targetColor);
		SkyTexture.material.color = targetColor;
		while (fadeInTimer < fadeInTime) {

			fadeInTimer += Time.deltaTime;
			targetColor.a = fadeInTimer/1;
			SkyTexture.material.color = targetColor;
			yield return null;
		}
	}
}
