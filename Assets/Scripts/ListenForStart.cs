using UnityEngine;
using System.Collections;

public class ListenForStart : MonoBehaviour {
	[SerializeField] private GameObject flash;
	private bool triggered = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.Joystick1Button7)||Input.GetKey(KeyCode.Return))
		{
			if(triggered == false)
			{
				StartCoroutine("Transition");
			}
		}
	}

	private IEnumerator Transition()
	{
		flash.GetComponent<WhiteFlash> ().RequestFlash();
		yield return new WaitForSeconds (1);
		Application.LoadLevel("SHIPFadeIn");
	}
}
