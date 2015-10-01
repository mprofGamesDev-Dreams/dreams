using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public enum ActivePower{Imagi,Logio,Void};

public class ActivePowerUI : MonoBehaviour {
	public Color32 ImagiC = new Color32(86,255,255,255);
	public Color32 LogioC = new Color32(255,45,45,255);
	public Color32 VoidC = new Color32(190,45,255,255);
	public Image uiIndicator;

	[SerializeField]private ActivePower currentPower;
	private AbilityBehaviours abilityBehaviours;
	private InputHandler input;

	[SerializeField]private ResourceBar powerBar;

	public static ActivePowerUI instance = null;

	// Use this for initialization
	void Start () 
	{
		if (instance == null)
			instance = this;

		abilityBehaviours = GameObject.Find ("Player").GetComponent<AbilityBehaviours> ();
		input = GameObject.Find ("Player").GetComponent<InputHandler> ();
		uiIndicator.color = ImagiC;
	}
	
	// Update is called once per frame
	void Update () {

		currentPower = abilityBehaviours.getCurrentPower();
		/*if (input.isPrevPower() || input.isNextPower()) {
			currentPower = abilityBehaviours.getCurrentPower();
		}*/


		/*if (CrossPlatformInputManager.GetButtonDown ("Fire1"))//LOGIO
		{
			currentPower = ActivePower.Logio;
		}
		else if (CrossPlatformInputManager.GetButtonDown ("Fire2"))//IMAGI
		{
			currentPower = ActivePower.Imagi;
		}
		else if (CrossPlatformInputManager.GetButtonDown ("Fire3"))//VOID
		{
			currentPower = ActivePower.Void;
		}*/

		switch (currentPower) 
		{
		case ActivePower.Logio:
			uiIndicator.color = LogioC;
			powerBar.BarType = ResourceType.Logio;
			break;
		case ActivePower.Imagi:
			uiIndicator.color = ImagiC;
			powerBar.BarType = ResourceType.Imagi;
			break;
		case ActivePower.Void:
			uiIndicator.color = VoidC;
			powerBar.BarType = ResourceType.Void;
			break;

		}

	}

	public ActivePower CurrentPower
	{
		get{return currentPower;}
		set{currentPower = value;}
	}
}
