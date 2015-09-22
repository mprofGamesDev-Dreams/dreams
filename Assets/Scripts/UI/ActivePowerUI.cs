﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum ActivePower{Imagi,Logio,Void};

public class ActivePowerUI : MonoBehaviour {
	private Color32 ImagiC = new Color32(86,255,255,255);
	private Color32 LogioC = new Color32(255,45,45,255);
	private Color32 VoidC = new Color32(190,45,255,255);
	public Image uiIndicator;

	private ActivePower currentPower;
	// Use this for initialization
	void Start () 
	{
		currentPower = ActivePower.Imagi;
		uiIndicator.color = ImagiC;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Alpha1))//LOGIO
		{
			currentPower = ActivePower.Logio;
			uiIndicator.color = LogioC;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))//IMAGI
		{
			currentPower = ActivePower.Imagi;
			uiIndicator.color = ImagiC;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))//VOID
		{
			currentPower = ActivePower.Void;
			uiIndicator.color = VoidC;
		}

	}
}