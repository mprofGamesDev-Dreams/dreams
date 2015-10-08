using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

//public enum ActivePower{Imagi,Logio,Void};

public class ActivePowerManager : MonoBehaviour {
	/*
	public Color32 ImagiC = new Color32(86,255,255,255);
	public Color32 LogioC = new Color32(255,45,45,255);
	public Color32 VoidC = new Color32(190,45,255,255);*/
	[SerializeField] private Sprite logioImage;
	[SerializeField] private Sprite imagiImage;
	[SerializeField] private Sprite voidImage;

	[SerializeField] private Sprite logioCross;
	[SerializeField] private Sprite imagiCross;
	[SerializeField] private Sprite voidCross;

	[SerializeField] private Image uiBar;
	[SerializeField] private Image uiCross;
	[SerializeField] private GameObject barManager;

	[SerializeField]private ActivePower currentPower;
	private AbilityBehaviours abilityBehaviours;
	//private InputHandler input;
	
	[SerializeField]private ResourceBar powerBar;
	
	public static ActivePowerManager instance = null;
	
	// Use this for initialization
	void Start () 
	{
		if (instance == null)
			instance = this;
		
		abilityBehaviours = GameObject.Find ("Player").GetComponent<AbilityBehaviours> ();
		powerBar = barManager.GetComponent<ResourceBar> ();
		//input = GameObject.Find ("Player").GetComponent<InputHandler> ();
	//	uiIndicator.color = ImagiC;
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
			//uiIndicator.color = LogioC;
			uiBar.sprite = logioImage;
			uiCross.sprite = logioCross;
			powerBar.BarType = ResourceType.Logio;
			break;
		case ActivePower.Imagi:
			//uiIndicator.color = ImagiC;
			uiBar.sprite = imagiImage;
			uiCross.sprite = imagiCross;
			powerBar.BarType = ResourceType.Imagi;
			break;
		case ActivePower.Void:
			//uiIndicator.color = VoidC;
			uiBar.sprite = voidImage;
			uiCross.sprite = voidCross;
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
