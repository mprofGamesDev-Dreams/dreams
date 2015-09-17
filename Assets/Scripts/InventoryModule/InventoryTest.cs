using UnityEngine;
using System.Collections;
using InventoryModule;

public class InventoryTest : MonoBehaviour 
{
	[SerializeField] private Vector3 spawnPosition;

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			A_Item item = InventoryModule.InventoryManager.Instance.GetItemAt( 0 );

			if(item != null)
					item.DropItem( spawnPosition );
		}

		if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			A_Item item = InventoryModule.InventoryManager.Instance.GetItemAt( 0 );
			
			if(item != null)
				item.DestoryItem( );
		}
	}
}
