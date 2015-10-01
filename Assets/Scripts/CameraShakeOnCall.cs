using UnityEngine;
using System.Collections;

public class CameraShakeOnCall : MonoBehaviour 
{

	[Tooltip("Angle Variance From Center (0)")]
	[SerializeField] private float angleVariance = 20;

	[Tooltip("% used to reduce angle over time")]
	[SerializeField] [Range(0.90f,1)] private float angleDampner = 0.96f;

	[Tooltip("Speed of Screen Shake")]
	[SerializeField] private float speed = 15;

	[Tooltip("Maximum Time Constraint")]
	[SerializeField] private float duration = 0.75f;

	[Tooltip("Cameras To Shake")]
	[SerializeField] private Transform[] cameras;

	// Current Dampened Angle Per Frame
	private float dampenedAngle;

	// Time Start
	private float startTime;
	
	private void Update () 
	{
		if(Input.GetKeyDown(KeyCode.L))
			ShakeViewport();

		if( Time.time < (startTime + duration) )
		{
			for(int i = 0; i < cameras.Length; i++)
			{
				cameras[i].rotation = Quaternion.Euler( cameras[i].rotation.eulerAngles.x, cameras[i].rotation.eulerAngles.y,( Mathf.Sin( Time.time * speed ) ) * dampenedAngle );  
				dampenedAngle *= angleDampner;
			}

		}else 
		{
			for(int i = 0; i < cameras.Length; i++)
			{
				cameras[i].rotation = Quaternion.Euler( cameras[i].rotation.eulerAngles.x, cameras[i].rotation.eulerAngles.y, 0 );
			}
		}
	}

	public void ShakeViewport()
	{
		if( Time.time > (startTime + duration) )
		{
			startTime = Time.time;
			dampenedAngle = angleVariance;
		}
	}
}
