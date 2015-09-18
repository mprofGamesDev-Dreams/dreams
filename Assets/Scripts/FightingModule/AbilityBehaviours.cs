using UnityEngine;
using System.Collections;

public class AbilityBehaviours : MonoBehaviour 
{
	// Serialize Private Fields To Show Up In The Insepctor
	[SerializeField] private float attackRange = 100.0f;

	[SerializeField] private ParticleSystem myParticleSystem;

	private Transform myCameraTransform;
	private Color32 beamColor;
	ActivePower currentPower;
	private void Start () 
	{
		// Gets The Main Camera's Transform On Object Startup
		myCameraTransform = Camera.main.GetComponent<Transform>();
		currentPower = ActivePower.Imagi;
	}
	
	private void Update () 
	{		
		if (Input.GetKeyDown(KeyCode.Alpha1))//LOGIO
		{
			currentPower = ActivePower.Logio;
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha2))//IMAGI
		{
			currentPower = ActivePower.Imagi;
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha3))//VOID
		{
			currentPower = ActivePower.Void;
		}

		if (Input.GetMouseButtonDown (1)) 
		{
			shootRay();
		}
	}

	private void shootRay()
	{

		switch(currentPower)
		{
		case ActivePower.Logio:
			beamColor = Color.yellow;
			break;
		case ActivePower.Imagi:
			beamColor = Color.cyan;
			break;
		case ActivePower.Void:
			beamColor = Color.blue;
			break;
		}
		
		RaycastHit hit;
		Ray pointer = new Ray(myCameraTransform.position, myCameraTransform.forward * attackRange);
		
		Debug.DrawRay(pointer.origin, pointer.direction, beamColor);
		
		myParticleSystem.startColor = beamColor;
		myParticleSystem.Emit(1);
		
		if (Physics.Raycast(pointer, out hit, attackRange))
		{
			Shield shieldScript = hit.transform.GetComponent<Shield>();
			if(shieldScript != null)
			{
				switch(currentPower)
				{
				case ActivePower.Logio:
					shieldScript.ApplyDamage(Shield.shieldOptions.Logio, 10	);
					break;
				case ActivePower.Imagi:
					shieldScript.ApplyDamage(Shield.shieldOptions.Imagi, 10	);
					break;
				case ActivePower.Void:
					shieldScript.ApplyDamage(Shield.shieldOptions.Void, 10	);
					break;
					
				}
			}
		}
	}
}



