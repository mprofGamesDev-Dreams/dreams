using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour {

	WhiteFlash flash;
	// Use this for initialization
	void Start () {
		flash = GetComponent<WhiteFlash>();
		flash.RequestFlash();
	}
}
