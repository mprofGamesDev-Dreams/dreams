using UnityEngine;
using System.Collections;

/*
 * Transforms the position and scale of one object to another
 * 
 * ATTACHMENT: Attach to an object and specify a transform target
 * 
 * VARIABLES:
 * 		TransformTarget - GameObject transform that the object will move to
 * 		AxisOrder - Customise the order that the object translates in
 * 		MovementSpeed - How fast the object will move
 * 
 */

public class TransformGeometry : MonoBehaviour
{
	// Target transform to move geomtry to
	public Transform[] TransformTarget;
	public int targetIndex = 0;
	public bool isPathLooping = true;

	public float MovementSpeed = 5;

	private int TransformStage = 1;

    public bool triggered = false;

	void Start()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		if (targetIndex >= TransformTarget.Length) {
			if(isPathLooping)
				targetIndex = 0;
			else
				return;
		}

        if (triggered)
        {
            if (TransformStage == 1)
            {
                TransformObject();
            }
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, TransformTarget[targetIndex].localScale, MovementSpeed * Time.deltaTime);
            }
        }
	}

	private void TransformObject()
	{
		// Moving Platform
		Vector3 CurrentPosition = transform.position;

		// Target Position
		Vector3 TargetPosition = transform.TransformPoint(TransformTarget[targetIndex].localPosition);

		Vector3 NewPosition = Vector3.MoveTowards(CurrentPosition, TargetPosition, MovementSpeed * Time.deltaTime);

		// Store children information

		Vector3 EndTargetPos = TransformTarget[targetIndex].position;
		
		transform.position = NewPosition;

		TransformTarget[targetIndex].transform.position = EndTargetPos;

		if(transform.position.Equals (TargetPosition))
		   targetIndex++;
	}

    public void Trigger()
    {
        triggered = true;
    }

	public void Reset()
	{
		triggered = false;
		targetIndex = 0;
	}

	public bool IsTriggered()
	{
		return triggered;
	}

	public bool CheckPosition(int WaypointID)
	{
		return transform.position.Equals(TransformTarget[WaypointID].position);
	}
}
