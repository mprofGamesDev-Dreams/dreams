using UnityEngine;
using System.Collections;

public class AbilityBehaviours : MonoBehaviour 
{
	// Serialize Private Fields To Show Up In The Insepctor
	[SerializeField] private float attackRange = 100.0f;
	
	[SerializeField] private ParticleSystem myParticleSystem;

	private Transform myCameraTransform;

	private void Start () 
	{
		// Gets The Main Camera's Transform On Object Startup
		myCameraTransform = Camera.main.GetComponent<Transform>();
	}
	
	private void Update () 
	{		
		if (Input.GetKeyDown(KeyCode.L))//LOGIO
		{
			RaycastHit hit;
			Ray pointer = new Ray(myCameraTransform.position, myCameraTransform.forward * attackRange);
			
			Debug.DrawRay(pointer.origin, pointer.direction, Color.black);
			
			myParticleSystem.startColor = Color.yellow;
			myParticleSystem.Emit(1);
			
			if (Physics.Raycast(pointer, out hit, attackRange))
			{
				Shield shieldScript = hit.transform.GetComponent<Shield>();
				if(shieldScript != null)
					shieldScript.ApplyDamage(Shield.shieldOptions.Logio, 10	);
			}
		}
		
		if (Input.GetKeyDown(KeyCode.M))//IMAGI
		{
			RaycastHit hit;
			Ray pointer = new Ray(myCameraTransform.position, myCameraTransform.forward * attackRange);
			
			Debug.DrawRay(pointer.origin, pointer.direction, Color.black);
			
			myParticleSystem.startColor = Color.cyan;
			myParticleSystem.Emit(1);
			
			if (Physics.Raycast(pointer, out hit, attackRange))
			{
				Shield shieldScript = hit.transform.GetComponent<Shield>();
				if(shieldScript != null)
					shieldScript.ApplyDamage(Shield.shieldOptions.Imagi, 10	);
			}
		}
		
		if (Input.GetKeyDown(KeyCode.V))//VOID
		{
			RaycastHit hit;
			Ray pointer = new Ray(myCameraTransform.position, myCameraTransform.forward * attackRange);
			
			Debug.DrawRay(pointer.origin, pointer.direction, Color.black);
			
			myParticleSystem.startColor = Color.blue;
			myParticleSystem.Emit(1);
			
			if (Physics.Raycast(pointer, out hit, attackRange))
			{
				Shield shieldScript = hit.transform.GetComponent<Shield>();
				if(shieldScript != null)
					shieldScript.ApplyDamage(Shield.shieldOptions.Void, 10	);
			}
		}
	}
}
