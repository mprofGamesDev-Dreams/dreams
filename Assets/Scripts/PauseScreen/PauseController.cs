using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{

	// Array of menu item control names.
	private Button[] menuOptions = new Button[3];
	// Current button selected by player (by default "resume")
	private int selectedIndex = 0;
	// Prevents player from scrolling through menu items too fast
	private const float scrollDelay = 0.3f;
	private float nextScrollAvailable;
	[SerializeField] private Button resumeButton;
	[SerializeField] private Button restartButton;
	[SerializeField] private Button quitButton;
	// Arrow to show button player can currently select
	[SerializeField] private RectTransform arrowPosition;
	// Offset between arrow and menu option
	private Vector2 arrowOffset = new Vector2 (10.0f, -22.0f);
	// Change in heights between menu options; used to move arrow correctly
	//private float menuOptionsDifference = 50.0f;

	void Awake ()
	{
		menuOptions [0] = resumeButton;
		menuOptions [1] = restartButton;
		menuOptions [2] = quitButton;
	}

	void updateArrowPosition ()
	{
		// Arrow position calculated by subtracting offset from selected button's position
		Vector2 pos = menuOptions [selectedIndex].GetComponentInParent<RectTransform> ().anchoredPosition - arrowOffset;
		arrowPosition.anchoredPosition = pos;
	}
	
	
	// Function to scroll through possible menu items array, looping back to start/end depending on direction of movement
	void scrollMenu (String direction)
	{
		if (direction == "up") {
			if (selectedIndex == 0) {
				selectedIndex = menuOptions.Length - 1;
			} else {
				selectedIndex -= 1;
			}
		}
		
		if (direction == "down") {
			if (selectedIndex == menuOptions.Length - 1) {
				selectedIndex = 0;
			} else {
				selectedIndex += 1;
			}
		}

		updateArrowPosition ();
		nextScrollAvailable = Time.realtimeSinceStartup + scrollDelay;
	}

	// Checks if player has pressed "A" to select a menu option
	void CheckForSelection ()
	{
		if (CrossPlatformInputManager.GetButtonDown ("Jump")) {
			menuOptions [selectedIndex].onClick.Invoke ();
		}
	}
	
	void Update ()
	{

		float vertical = CrossPlatformInputManager.GetAxisRaw ("Vertical");

		// Check if enough time has passed to allow for an update in the selected index
		if (Time.realtimeSinceStartup >= nextScrollAvailable) {

			if (vertical < -0.5) {
				scrollMenu ("down");
			}
		
			if (vertical > 0.5) {
				scrollMenu ("up");
			}
		}

		CheckForSelection ();
	}
}
