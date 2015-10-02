using UnityEngine;
using System.Collections;

public class TriggerPlatformOnDeath : MonoBehaviour
{
	public GameObject Platform;

	// Update is called once per frame
	void Update ()
	{
		EnemyScript EnemyTrigger = gameObject.GetComponent<EnemyScript> ();
		if (EnemyTrigger.Health < EnemyTrigger.MaxHealth * 0.5)
		{
			Platform.GetComponent<TransformGeometry>().Trigger();
		}
	}
}
