using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour 
{
	public float maxTime;
	private float startTime;
	public float maxAngle;
	public float shakeSpeed;

	private float targetAngle;

	private bool shakeScreen; 
	public bool ShakeScreen 
	{ 
		set 
		{ 
			shakeScreen = true; 
			startTime = Time.time; 

			float f = Mathf.Sign(Random.Range(-1,1));
			if(f != 0)
				targetAngle = f * maxAngle;
		} 
	}

	public Transform[] cameras;
	
	public void Update()
	{
		if(Input.GetKeyDown(KeyCode.L))
			ShakeScreen = true;

		if(shakeScreen)
		{
			if( Time.time < (startTime + maxTime) )
			{
				for(int i = 0; i < cameras.Length; i++)
				{
					if( (targetAngle < 0 && cameras[i].rotation.eulerAngles.z < targetAngle) && (targetAngle > 0 && cameras[i].rotation.eulerAngles.z > targetAngle) )
						targetAngle *= -1;

					cameras[i].rotation = Quaternion.Euler( cameras[i].rotation.eulerAngles.x, cameras[i].rotation.eulerAngles.y, cameras[i].rotation.eulerAngles.z + targetAngle * shakeSpeed * Time.deltaTime );
				}
			}else shakeScreen = false;
		}else 
		{
			for(int i = 0; i < cameras.Length; i++)
			{
				cameras[i].rotation = Quaternion.Euler( cameras[i].rotation.eulerAngles.x, cameras[i].rotation.eulerAngles.y, 0 );
			}
		}
	}
}
