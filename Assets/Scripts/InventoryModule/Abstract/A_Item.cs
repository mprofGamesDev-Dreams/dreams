using UnityEngine;
using System.Collections;

namespace InventoryModule
{
	public abstract class A_Item : MonoBehaviour, I_ItemInteractions
	{
		// Force Class To Have A "Sprite" For The UI
		[SerializeField] private Sprite uiTexture = null;

		// Function To Handle Picking Up an Item
		public void PickupItem( )
		{
			InventoryManager.Instance.AddItem( this );
			this.gameObject.SetActive( false );
		}

		// Function To Handle Dropping An Item
		public void DropItem( Vector3 newPosition )
		{
			InventoryManager.Instance.RemoveItem( this );

			this.GetComponent<Transform>().position = newPosition;
			this.gameObject.SetActive(true);
		}

		// Abstract Class To Implement Some Behaviour
		public abstract void InteractBehaviour( );

		// Function To Handle The Destruction Of An Item
		public void DestoryItem( )
		{
			InventoryManager.Instance.RemoveItem( this );

			Destroy( this.gameObject );
		}

		// Property of the UITexture
		public Sprite UITexture
		{
			get{ return this.uiTexture;}
			set{ this.uiTexture = value; }
		}
	}
}