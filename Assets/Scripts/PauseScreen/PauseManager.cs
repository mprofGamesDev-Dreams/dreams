using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class PauseManager : MonoBehaviour 
{
	
	public bool is_paused;
	
	// Pause pop-up
	[SerializeField] private GameObject pause_screen;
	[SerializeField] private GameObject pause_menu;
	[SerializeField] private GameObject HUD;
	
	// Use this for initialization
	void Awake () 
	{
		// Initialise canvases to correspond to a main menu greeting the player
		is_paused = false;
		
		// Pause screen and its menus
		SetInitialPauseMenu ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Setup Pause key for pausing the game
		if(CrossPlatformInputManager.GetButtonDown ("Pause"))
		{
			is_paused = !is_paused;
			if(is_paused) 
			{
				PauseGame();
			}
			else
			{
				ResumeGame();
				SetInitialPauseMenu();
			}
		}
	}
	
	private void SetInitialPauseMenu()
	{
		pause_screen.SetActive (false);
		pause_menu.SetActive (true);
	}
	
	public void PauseGame()
	{
		is_paused = true;
		pause_screen.SetActive (is_paused);
		HUD.SetActive (!is_paused);
		Time.timeScale = 0.0f;
	}
	
	public void ResumeGame()
	{
		is_paused = false;
		pause_screen.SetActive (is_paused);
		HUD.SetActive (!is_paused);
		Time.timeScale = 1.0f;
	}

	public void RestartGame()
	{
		Application.LoadLevel (Application.loadedLevel);
		ResumeGame ();
	}

	public void QuitGame(){
		Debug.Log ("Quit triggered");
		Application.Quit ();
	}
	
	public void BackToPauseMenu(GameObject to_hide)
	{
		to_hide.SetActive (false);
		pause_menu.SetActive (true);
	}
	
	public void OpenFromPause(GameObject to_show)
	{
		to_show.SetActive (true);
		pause_menu.SetActive (false);
	}
	
}
