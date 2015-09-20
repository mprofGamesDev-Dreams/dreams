using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour 
{
	
	[SerializeField] private GameObject options_panel;

	// Use this for initialization
	void Awake () {
		options_panel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NewGame(){
		Application.LoadLevel ("FirstLevelConcept");
	}
	
	public void LoadGame(){
		// TODO: Load most recently reached level
		Application.LoadLevel ("FirstLevelConcept");
	}

	public void QuitGame(){
		Application.Quit ();
	}

}
