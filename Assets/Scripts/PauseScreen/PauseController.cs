using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{

	// Array of menu item control names.
	private String[] menuOptions = new String[4];
	// Current button selected by player (by default "resume")
	int selectedIndex = 0;
	// Prevents player from scrolling through menu items too fast
	private const float scrollDelay = 0.5f;
	private float nextScrollAvailable;
	[SerializeField] private Button ResumeButton;
	[SerializeField] private Button RestartButton;
	[SerializeField] private Button HelpButton;
	[SerializeField] private Button QuitButton;

	void Awake ()
	{
		menuOptions [0] = "Resume";
		menuOptions [1] = "Restart";
		menuOptions [2] = "Help";
		menuOptions [3] = "Quit";
	}
	
	
	// Function to scroll through possible menu items array, looping back to start/end depending on direction of movement.
	int menuSelection (String[] menuItems, int selectedItem, String direction)
	{
		if (direction == "up") {
			if (selectedItem == 0) {
				selectedItem = menuItems.Length - 1;
			} else {
				selectedItem -= 1;
			}
		}
		
		if (direction == "down") {
			if (selectedItem == menuItems.Length - 1) {
				selectedItem = 0;
			} else {
				selectedItem += 1;
			}
		}

		nextScrollAvailable = Time.realtimeSinceStartup + scrollDelay;
		
		return selectedItem;
	}

	// Checks if player has pressed "A" to select a menu option
	void CheckForSelection ()
	{
		if (CrossPlatformInputManager.GetButtonDown ("Jump")) {

			switch (menuOptions [selectedIndex]) {
			case "Resume":
				ResumeButton.onClick.Invoke ();
				break;
			case "Restart":
				RestartButton.onClick.Invoke ();
				break;
			case "Help":
				HelpButton.onClick.Invoke ();
				break;
			case "Quit":
				QuitButton.onClick.Invoke ();
				break;
			}
		}
	}
	
	void Update ()
	{

		float vertical = CrossPlatformInputManager.GetAxisRaw ("Vertical");

		// Check if enough time has passed to allow for an update in the selected index
		if (Time.realtimeSinceStartup >= nextScrollAvailable) {

			if (vertical < -0.5) {
				selectedIndex = menuSelection (menuOptions, selectedIndex, "down");
			}
		
			if (vertical > 0.5) {
				selectedIndex = menuSelection (menuOptions, selectedIndex, "up");
			}
		}

		CheckForSelection ();
		// Uncomment below to show option selected in console
		// TODO: visually show which button is highlighted
		//Debug.Log (menuOptions[selectedIndex]);
	}
}
