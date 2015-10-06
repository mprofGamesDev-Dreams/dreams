using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour {
	[SerializeField] private float waitTime;
	private float startTime;
	WhiteFlash flash;
	// Use this for initialization
	void Start () {
		flash = GetComponent<WhiteFlash>();
		startTime = Time.time;
	}

	void Update()
	{
		if(Time.time > startTime + waitTime)
		{
			flash.fadeOutSpeed = 1;
			flash.RequestFlash();
			Destroy(this);
		}
	}
}
