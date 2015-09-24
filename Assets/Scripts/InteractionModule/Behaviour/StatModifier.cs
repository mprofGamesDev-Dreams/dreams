using UnityEngine;
using System.Collections;

namespace InteractionModule.Behaviours
{	
	public class StatModifier : A_InteractBehaviour 
	{
		public enum EPickupStatModifier { SelectOne, Health, Stamina, Void, Logio, Imagi }

		// Add Variables And MonoBehaviour Functions At Will
		// Remember To Add Custom Inspector Code To InteractEditor
		[SerializeField] private PlayerStats myPlayerStats;
		[SerializeField] private EPickupStatModifier pickupType;
		[SerializeField] private float pickupValue;

		public override void Initialize (A_InteractBehaviour behaviourParameters)
		{
			myPlayerStats = ((StatModifier)behaviourParameters).myPlayerStats;
			pickupType = ((StatModifier)behaviourParameters).pickupType;
			pickupValue = ((StatModifier)behaviourParameters).pickupValue;
		}
		
		public override void OnEventTrigger ()
		{
			switch(pickupType)
			{
				case EPickupStatModifier.SelectOne:
					Debug.LogError("Please Initialize PickupType At: " + this.gameObject.name);
					break;
				case EPickupStatModifier.Health:
					myPlayerStats.ModifyHealth(pickupValue);
					break;
				case EPickupStatModifier.Stamina:
					myPlayerStats.ModifyStamina(pickupValue);
					break;
				case EPickupStatModifier.Void:
					myPlayerStats.ModifyVoid(pickupValue);
					break;
				case EPickupStatModifier.Logio:
					myPlayerStats.ModifyLogio(pickupValue);
					break;
				case EPickupStatModifier.Imagi:
					myPlayerStats.ModifyImagi(pickupValue);
					break;
			}
			OnEventEnd ();
		}

		public override void OnEventEnd()
		{
			Destroy (this.gameObject);
		}

		#region Properties
		public PlayerStats MyPlayerStats {
			get {
				return this.myPlayerStats;
			}
			set {
				myPlayerStats = value;
			}
		}

		public EPickupStatModifier PickupType {
			get {
				return this.pickupType;
			}
			set {
				pickupType = value;
			}
		}

		public float PickupValue {
			get {
				return this.pickupValue;
			}
			set {
				pickupValue = value;
			}
		}
		#endregion
	}
}