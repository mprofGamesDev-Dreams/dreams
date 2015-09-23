using UnityEngine;
using System.Collections;

/*
 * Transforms the position and scale of one object to another
 * 
 * ATTACHMENT: Attach to an object and specify a transform target
 * 
 */

public class TransformGeometry : MonoBehaviour
{
	public Transform TransformTarget;

	// Specify a list of axis ( X Y Z ONLY )
	// This will be transform order
	public string[] AxisOrder = new string[3];

	public float MovementSpeed = 5;

	private int AxisComplete = 0;

	private string TargetAxis;

	private int TransformStage = 1;

	void Start()
	{
		TargetAxis = AxisOrder[AxisComplete];
	}

	// Update is called once per frame
	void Update ()
	{
		if(TransformStage == 1)
		{
			TransformObject ();
		}
		else
		{
			transform.localScale = Vector3.Lerp (transform.localScale, TransformTarget.localScale, MovementSpeed * Time.deltaTime);
		}
	}

	private void TransformObject()
	{
		Vector3 CurrentPosition = new Vector3(0,0,0);
		Vector3 TargetPosition = new Vector3(0,0,0);

		switch(TargetAxis)
		{
			case "x":
				CurrentPosition.x = transform.position.x;
				TargetPosition.x = TransformTarget.position.x;
				break;

			case "y":
				CurrentPosition.y = transform.position.y;
				TargetPosition.y = TransformTarget.position.y;
				break;

			case "z":
				CurrentPosition.z = transform.position.z;
				TargetPosition.z = TransformTarget.position.z;
				break;
		}

		Vector3 NewPosition = Vector3.MoveTowards(CurrentPosition, TargetPosition, MovementSpeed * Time.deltaTime);

		switch(TargetAxis)
		{
			case "x":
				transform.position = new Vector3(NewPosition.x, transform.position.y, transform.position.z);
				
				if( transform.position.x.Equals(TargetPosition.x) )
				{
					UpdateAxisTarget();
				}
				break;

			case "y":
				transform.position = new Vector3(transform.position.x, NewPosition.y, transform.position.z);
				
				if( transform.position.y.Equals(TargetPosition.y) )
				{
					UpdateAxisTarget();
				}
				break;

			case "z":
				transform.position = new Vector3(transform.position.x, transform.position.y, NewPosition.z);
				
				if( transform.position.z.Equals(TargetPosition.z) )
				{
					UpdateAxisTarget();
				}
			break;
		}
	}

	private void UpdateAxisTarget()
	{
		// Move to next target
		AxisComplete++;

		// Make sure to stay within range
		if(AxisComplete < AxisOrder.Length)
		{
			TargetAxis = AxisOrder[AxisComplete];
		}
		else
		{
			TransformStage = 2;
		}
	}
}
