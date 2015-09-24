using UnityEngine;
using System.Collections;
using InteractionModule;

/// <summary>
/// WIP needs some optimization
/// </summary>
public class InteractionController : MonoBehaviour 
{
	[SerializeField] private float interactionDistance = 0;

	[SerializeField] private LayerMask raycastMask;

	private int lastGameObjectID = 0;
	private Interact interactableObject = null;

	private Transform cameraTransform;

	private void Start()
	{
		cameraTransform = Camera.main.GetComponent<Transform>();
	}

	private void Update()
	{
		// Raycast Hit Information
		RaycastHit hit;

		// Debug To See Where The Ray Currently Is

		// Raycast To Search For Interactable Objects
		// On Fail. It Just Resets Parameters
		// On Success Check That The Object Is Interactable
		//	- If Not Then Reset Parameters
		//  - On Success Show UI And Set Up Parameters
		if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, interactionDistance, raycastMask))
		{
			Debug.DrawLine(cameraTransform.position, hit.point, Color.green);

			int objID = hit.collider.gameObject.GetInstanceID();
			if(lastGameObjectID != objID)
			{
				if(interactableObject != null)
					interactableObject.HideInteractableUI();

				interactableObject = hit.collider.GetComponent<Interact>();

				if(interactableObject == null)
				{
					ResetParameters();
					return;
				}else
				{
					lastGameObjectID = objID;
					interactableObject.ShowInteractableUI();
					return;
				}
			}
		}else 
		{
			Debug.DrawRay(cameraTransform.position, cameraTransform.forward * interactionDistance, Color.red);
			ResetParameters();
			return;
		}
	}

	private void ResetParameters()
	{
		if(interactableObject != null)
		{
			interactableObject.HideInteractableUI();
		}

		lastGameObjectID = 0;
		interactableObject = null;
	}
}
