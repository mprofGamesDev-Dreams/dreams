using UnityEngine;
using System.Collections;

public class GameLoopOLD : MonoBehaviour {

	public bool on_main_menu;
	public bool on_options;
	public bool is_paused;
	public GameObject main_menu;
	public GameObject options_panel;
	public GameObject ingame_ui;
	public CanvasGroup pause_button_controls;

	[SerializeField] private GameObject HUD;

	// Pause pop-up
	private GameObject pause_popup;
	private GameObject pause_menu;
	private GameObject pause_controls;
	private GameObject pause_options;

	// Use this for initialization
	void Awake () {
		// Initialise canvases to correspond to a main menu greeting the player
		on_main_menu = true;
		is_paused = true;
		main_menu = GameObject.Find ("Main Menu");
		options_panel = GameObject.Find ("Options Panel");
		ingame_ui = GameObject.Find ("In-Game UI");
		pause_button_controls = GameObject.Find("Pause Button").GetComponent<CanvasGroup>();

		HUD.SetActive (false);

		options_panel.SetActive (false);
		ingame_ui.SetActive (false);

		// Pause pop-up and its menus
		FindInitialPauseMenu ();
		SetInitialPauseMenu ();
	}

	// Update is called once per frame
	void Update () {
		// On main menu
		if (on_main_menu) {

		}
		// In game
		else{
			// Reserve "p" key for pausing game
			if(Input.GetKeyDown(KeyCode.Escape)){
				is_paused = !is_paused;
				pause_popup.SetActive(is_paused);
				if(is_paused) HidePauseButton();
				else{
					ShowPauseButton();
					SetInitialPauseMenu();
				}
			}
			
			// On pause menu
			if(is_paused){
				// Stop in-game objects from updating
			}
		}
	}

	private void FindInitialPauseMenu(){
		pause_popup = GameObject.Find ("Pause Pop-up");
		pause_menu = GameObject.Find ("Pause Menu");
		pause_controls = GameObject.Find ("Pause Controls");
		pause_options = GameObject.Find ("Pause Options");
	}

	private void SetInitialPauseMenu(){
		pause_popup.SetActive (false);
		pause_menu.SetActive (true);
		pause_controls.SetActive (false);
		pause_options.SetActive (false);
	}

	public void NewGame(){
		InitialGameSetup ();
		// Load first level
	}

	public void LoadGame(){
		InitialGameSetup ();
		// Load most recently reached level
	}

	public void InitialGameSetup(){
		on_main_menu = false;
		main_menu.SetActive (on_main_menu);
		ingame_ui.SetActive (!on_main_menu);
		is_paused = false;
		ShowPauseButton();
		HUD.SetActive (true);
	}

	public void PauseGame(){
		is_paused = true;
		pause_popup.SetActive (is_paused);
		HidePauseButton ();
	}

	public void UnpauseGame(){
		is_paused = false;
		pause_popup.SetActive (is_paused);
		ShowPauseButton ();
	}

	public void ReturnToMain(){
		on_main_menu = true;
		pause_popup.SetActive (false);
		ingame_ui.SetActive (false);
		main_menu.SetActive (on_main_menu);
	}

	public void QuitGame(){
		Application.Quit ();
	}

	public void HidePauseButton(){
		pause_button_controls.alpha = 0.5f;
		pause_button_controls.interactable = false;
	}
	
	public void ShowPauseButton(){
		pause_button_controls.alpha = 1.0f;
		pause_button_controls.interactable = true;
	}

	public void BackToPauseMenu(GameObject to_hide){
		to_hide.SetActive (false);
		pause_menu.SetActive (true);
	}

	public void OpenFromPause(GameObject to_show){
		to_show.SetActive (true);
		pause_menu.SetActive (false);
	}

}
