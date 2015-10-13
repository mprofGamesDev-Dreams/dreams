using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlackScreen : MonoBehaviour
{
	[SerializeField] private Image image;
	public float fadeInSpeed = 10f;         // Speed that the screen fades to black
	public float fadeOutSpeed = 2.0f;			// Speed that the screen fades to clear
	public bool flashRequested = false;
	public bool toBlack;
	private float delay;
	
	void Awake ()
	{
		
	}
	
	void Update ()
	{
		if (flashRequested) {
			// Check if fading to or from black
			if (toBlack) 
			{
				// Continue fading until image is almost black
				if (image.color.a < 0.995) {
					FadeToBlack ();
				} 
				// Once image is almost black, set to pure black and delay slightly
				else {
					image.color = new Color (0.0f, 0.0f, 0.0f, 1.0f);
					toBlack = false;
					delay = Time.time + 0.5f;
				}
			} else if (Time.time > delay) 
			{
				// After brief delay, fade black to clear
				if (image.color.a > 0.005)
				{
					FadeToClear ();
				}
				// When image is almost fully clear, disable it
				else
				{
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
		toBlack = true;
	}
	
	public void FadeToClear ()
	{
		// Lerp the colour of the panel between itself and transparent
		image.color = Color.Lerp (image.color, Color.clear, fadeOutSpeed * Time.deltaTime);
	}
	
	public void FadeToBlack ()
	{
		// Lerp the colour of the panel between itself and black
		image.color = Color.Lerp (image.color, Color.black, fadeInSpeed * Time.deltaTime);
	}
}