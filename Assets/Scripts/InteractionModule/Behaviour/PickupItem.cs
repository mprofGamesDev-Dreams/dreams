using UnityEngine;
using System.Collections;
using InventoryModule;

namespace InteractionModule.Behaviours
{
	public class PickupItem : A_InteractBehaviour
	{
		public override void Initialize (A_InteractBehaviour behaviourParameters)
		{
			
		}
		
		public override void OnEventTrigger ()
		{
			this.GetComponent<Item>().PickupItem();
		}
		
		public override void OnEventEnd ()
		{
			// Do Nothing
		}
	}
}