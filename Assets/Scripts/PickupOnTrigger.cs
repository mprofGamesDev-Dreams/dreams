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
	[SerializeField] private AudioClip pickupSound;
	[SerializeField] private GameObject playerEffect;

	private void Start () 
	{
		GameObject player = GameObject.FindGameObjectWithTag ("Player");

		playerEffect = player.transform.Find ("AudioSourcePickup").gameObject;
		if (!playerEffect) {
			Debug.Log("Someone changed the pickup audio source. Please view PickupOnTrigger.cs Stert()");
		}
	}
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
			playerEffect.GetComponent<AudioSource>().PlayOneShot(pickupSound,1.0f);
			gameObject.SetActive(false);
			//Destroy(this.gameObject);
		}

	}

	public PickupType StatRecoveryType {
		get {
			return this.statRecoveryType;
		}
		set {
			statRecoveryType = value;
		}
	}

	public float StatModifyValue {
		get {
			return this.statModifyValue;
		}
		set {
			statModifyValue = value;
		}
	}
}
