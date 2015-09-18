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
			A_Item item = this.gameObject.GetComponent< A_Item >();//.PickupItem();
			if(item == null)
				Debug.Log("Error");
			else item.PickupItem();
		}
		
		public override void OnEventEnd ()
		{
			// Do Nothing
		}
	}
}