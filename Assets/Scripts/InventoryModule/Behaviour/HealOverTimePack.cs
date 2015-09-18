using UnityEngine;
using System.Collections;

namespace InventoryModule
{
	public class HealOverTimePack : A_ItemBehaviour
	{
		// Variables to make it work
		[SerializeField] private float healAmountPerIncrement;
		[SerializeField] private float maxTime;
		[SerializeField] private float timeIncrements;

		// Initializes Object
		public override void Initialize(Item parent, A_ItemBehaviour behaviourParameters)
		{
			this.parentReference = parent;

			healAmountPerIncrement = ((HealOverTimePack) behaviourParameters).HealAmount;
			maxTime = ((HealOverTimePack) behaviourParameters).MaxTime;
			timeIncrements = ((HealOverTimePack) behaviourParameters).TimeIncrements;
		}

		// Item Logic To Act
		public override void ItemAction()
		{
			StartCoroutine(HealOverTime(Time.time + maxTime, timeIncrements));

		}

		// Ie numerator with a heal over time
		private IEnumerator HealOverTime(float targetTime, float timeIncrements)
		{
			while(Time.time < targetTime)
			{
				GameObject obj = GameObject.FindGameObjectWithTag("Player");
				
				obj.GetComponent<PlayerStats>().Health += healAmountPerIncrement;

				yield return new WaitForSeconds(timeIncrements);
			}

			InventoryManager.Instance.RemoveItem(parentReference);
			parentReference.DestoryItem();
		}

		#region Properties
		public float HealAmount {
			get {
				return this.healAmountPerIncrement;
			}
			set {
				healAmountPerIncrement = value;
			}
		}

		public float MaxTime {
			get {
				return this.maxTime;
			}
			set {
				maxTime = value;
			}
		}

		public float TimeIncrements {
			get {
				return this.timeIncrements;
			}
			set {
				timeIncrements = value;
			}
		}
		#endregion
	}
}

