using UnityEngine;
using System.Collections;

public class PickupOnTrigger : MonoBehaviour 
{
	public enum PickupType
	{
		Logio,
		Imagi,
		Void,
		EXP
	}
	[SerializeField] private PickupType statRecoveryType; 
	[SerializeField] private float statModifyValue;

	private void OnTriggerEnter(Collider c)
	{
		if (c.gameObject.CompareTag ("Player")) 
		{
			switch( statRecoveryType )
			{
				case PickupType.Logio:
					c.GetComponent<PlayerStats>().ModifyLogio(statModifyValue);
					break;
				case PickupType.Imagi:
					c.GetComponent<PlayerStats>().ModifyImagi(statModifyValue);
					break;
				case PickupType.Void:
					c.GetComponent<PlayerStats>().ModifyVoid(statModifyValue);
					break;
				case PickupType.EXP: // not doing anything yet
					break;
			}

			Destroy(this.gameObject);
		}

	}
}
