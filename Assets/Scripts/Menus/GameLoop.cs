using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class GameLoop : MonoBehaviour 
{

	public bool is_paused;
	public GameObject ingame_ui;
	public CanvasGroup pause_button_controls;

	// Pause pop-up
	[SerializeField] private GameObject pause_popup;
	[SerializeField] private GameObject pause_menu;
	[SerializeField] private GameObject pause_controls;
	[SerializeField] private GameObject pause_options;

	// Use this for initialization
	void Awake () 
	{
		// Initialise canvases to correspond to a main menu greeting the player
		is_paused = false;
		ingame_ui = GameObject.Find ("In-Game UI");

		// Pause pop-up and its menus
		FindInitialPauseMenu ();
		SetInitialPauseMenu ();
	}

	// Update is called once per frame
	void Update () 
	{
			// Reserve "Esc" key for pausing game
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				is_paused = !is_paused;
				pause_popup.SetActive(is_paused);
				if(is_paused) 
				{
					HidePauseButton();
				}
				else
				{
					ShowPauseButton();
					SetInitialPauseMenu();
					// unpause game
					Time.timeScale = 1.0f;
				}
			}
			
			// On pause menu
			if(is_paused)
			{
				// Stop in-game objects from updating
				Time.timeScale = 0.0f;
			}
	}

	private void FindInitialPauseMenu()
	{
		pause_popup = GameObject.Find ("Pause Pop-up");
		pause_menu = GameObject.Find ("Pause Menu");
		pause_controls = GameObject.Find ("Pause Controls");
		pause_options = GameObject.Find ("Pause Options");
	}

	private void SetInitialPauseMenu()
	{
		pause_popup.SetActive (false);
		pause_menu.SetActive (true);
		pause_controls.SetActive (false);
		pause_options.SetActive (false);
	}

	public void NewGame()
	{
		InitialGameSetup ();
		// TODO: Load first level
	}

	public void LoadGame()
	{
		InitialGameSetup ();
		// TODO: Load most recently reached level
	}

	public void InitialGameSetup()
	{
		ingame_ui.SetActive (true);
		is_paused = false;
		ShowPauseButton();
	}

	public void PauseGame()
	{
		is_paused = true;
		pause_popup.SetActive (is_paused);
		HidePauseButton ();
		Time.timeScale = 0.0f;
	}

	public void UnpauseGame()
	{
		is_paused = false;
		pause_popup.SetActive (is_paused);
		ShowPauseButton ();
		Time.timeScale = 1.0f;
	}

	public void ReturnToMain()
	{
		UnpauseGame();
		Application.LoadLevel ("TitleScreen");
	}

	public void HidePauseButton()
	{
		pause_button_controls.alpha = 0.5f;
		pause_button_controls.interactable = false;
	}
	
	public void ShowPauseButton()
	{
		pause_button_controls.alpha = 1.0f;
		pause_button_controls.interactable = true;
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
