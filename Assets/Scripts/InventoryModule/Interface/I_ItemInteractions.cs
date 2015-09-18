using UnityEngine;
using System.Collections;

namespace InventoryModule
{
	public interface I_ItemInteractions 
	{
		void PickupItem( );
		void DropItem( Vector3 newPosition );
		void InteractBehaviour();
		void DestoryItem( );
	}
}