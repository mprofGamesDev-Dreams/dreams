using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
public class PauseManager : MonoBehaviour 
{
	
	public bool is_paused;
	
	// Pause pop-up
	[SerializeField] private GameObject pause_screen;
	[SerializeField] private GameObject pause_menu;
	[SerializeField] private GameObject HUD;
	public float current_time;

	// Prevents player from scrolling through menu items too fast
//	private const float scrollDelay = 0.1f;
//	private float nextScrollAvailable;
	private bool scrollAlowed = true;

	public Button resumeButton;
	public Button restartButton;
	public Button quitButton;
	private Button selectedButton;

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
				selectButton(resumeButton);
			}
			else
			{
				ResumeGame();
				SetInitialPauseMenu();
			}
		}

		//Handle selection changes
		if (is_paused) 
		{
			float vertical = CrossPlatformInputManager.GetAxisRaw ("Vertical");
			if (scrollAlowed) {
				
				if (vertical < -0.5) {
					if(selectedButton == resumeButton)
						selectButton (restartButton);
					else if(selectedButton == restartButton)
						selectButton (quitButton);
					else if(selectedButton == quitButton)
						selectButton(resumeButton);

					scrollAlowed = false;
				}
				
				if (vertical > 0.5) {
					if(selectedButton == resumeButton)
						selectButton (quitButton);
					else if(selectedButton == restartButton)
						selectButton (resumeButton);
					else if(selectedButton == quitButton)
						selectButton(restartButton);

					scrollAlowed = false;
				}
			}
			else 
			{
				if(vertical >= -0.5 && vertical <= 0.5)
					scrollAlowed = true;
			}

			//Select option
			if(CrossPlatformInputManager.GetButton ("Jump"))
			{
				selectedButton.onClick.Invoke();
			}
		}

		current_time = Time.timeScale;
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

	private void selectButton(Button b)
	{
		if(selectedButton != null) selectedButton.GetComponentInChildren<Text> ().color = Color.white;
		b.GetComponentInChildren<Text>().color = Color.yellow;
		selectedButton = b;
	}
	
}
