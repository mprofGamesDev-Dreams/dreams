using UnityEngine;
using System.Collections;
/*
* Script to give animated pathway.
* 
* ATTACHMENT: Attach to each island object.
* 
* VARIABLES: 
*			NextJigsawPiece - The next island in the pathway.
*
*/

public class JigsawPiece : MonoBehaviour
{
	// The next piece to generate
	public GameObject NextJigsawPiece;

	// Flag on whether or not the trigger has been activated
	private bool IsActivated = false;

	// Flag on whether or not the next piece has been generated
	private bool NextPieceGenerated = false;

	// Access to the new instantiated object
	private GameObject NextJigsawPieceAccess;

	// Access to the new box collider
	private BoxCollider TriggerCollider;

	void Start ()
	{
		// Add a new trigger collider
		TriggerCollider = this.gameObject.AddComponent<BoxCollider>();
		TriggerCollider.isTrigger = true;
		TriggerCollider.size = new Vector3(TriggerCollider.size.x, 2, TriggerCollider.size.z);
	}

	void Update ()
	{
		// If we dont have a jigsaw piece, dont do anything
		if(!NextJigsawPiece)
			return;

		// If the trigger has been activated, dont do anything else
		if(!IsActivated)
			return;

		// If we have created the next piece, dont do anything else
		if(NextPieceGenerated)
			return;

		// Create a new gameobject for the next piece in the path
		NextJigsawPieceAccess = (GameObject)GameObject.Instantiate(NextJigsawPiece, NextJigsawPiece.transform.position - (Vector3.up * 10), NextJigsawPiece.transform.rotation);

		// Add the transform to specify where it needs to move to
		TransformGeometry Transformer = NextJigsawPieceAccess.AddComponent<TransformGeometry>();
		Transformer.TransformTarget[0] = NextJigsawPiece.transform;

		// Flag that we have generated it
		NextPieceGenerated = true;
	} 

	void OnTriggerEnter(Collider col)
	{
		// Make sure only the player can trigger
		if(col.gameObject.name != "Player")
			return;

		// Flag that we have activated the trigger
		IsActivated = true;

		// Remove the un-needed component
		Destroy(TriggerCollider);
	}
}
