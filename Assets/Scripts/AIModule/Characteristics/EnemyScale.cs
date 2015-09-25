using UnityEngine;
using System.Collections;

public class EnemyScale : MonoBehaviour 
{
	[SerializeField] private Vector3 targetScale;
	[SerializeField] private float scaleTime;
	private bool canGrow = true;
	private Transform myTransform;
	
	
	private void Start () 
	{
		myTransform = GetComponent<Transform>();
	}
	
	private void Update()
	{
		if (canGrow) 
		{
			myTransform.localScale = Vector3.Lerp( myTransform.localScale, targetScale, Time.deltaTime * scaleTime );
			if(myTransform.localScale == targetScale)
			{
				canGrow = !canGrow;
			}
		}
	}
}
