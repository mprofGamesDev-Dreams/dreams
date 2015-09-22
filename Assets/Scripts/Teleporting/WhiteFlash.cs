using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WhiteFlash : MonoBehaviour
{
	[SerializeField] private Image image;
	private float fadeInSpeed = 10f;         // Speed that the screen fades to white
	private float fadeOutSpeed = 2.0f;			// Speed that the screen fades to clear
	private bool flashRequested = false;
	private bool toWhite;
	private float delay;
	
	void Awake ()
	{

	}
	
	void Update ()
	{
		if (flashRequested) {
			// Check if fading to or from white
			if (toWhite) {
				// Continue fading until image is almost white
				if (image.color.a < 0.995) {
					FadeToWhite ();
				} 
				// Once image is almost white, set to pure white and delay slightly
				else {
					image.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
					toWhite = false;
					delay = Time.time + 0.5f;
				}
			} else if (Time.time > delay) {
				// After brief delay, fade white to clear
				if (image.color.a > 0.005) {
					FadeToClear ();
				}
				// When image is almost fully clear, disable it
				else{
					image.enabled = false;
					flashRequested = false;
				}
			}
		}
	}

	// Begins the flash process
	public void RequestFlash ()
	{
		flashRequested = true;
		image.enabled = true;
		toWhite = true;
	}
	
	public void FadeToClear ()
	{
		// Lerp the colour of the panel between itself and transparent
		image.color = Color.Lerp (image.color, Color.clear, fadeOutSpeed * Time.deltaTime);
	}
	
	public void FadeToWhite ()
	{
		// Lerp the colour of the panel between itself and white
		image.color = Color.Lerp (image.color, Color.white, fadeInSpeed * Time.deltaTime);
	}
}