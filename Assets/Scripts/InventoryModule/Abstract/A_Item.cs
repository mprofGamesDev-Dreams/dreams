using UnityEngine;
using System.Collections;

namespace InventoryModule
{
	public abstract class A_Item : MonoBehaviour, I_ItemInteractions
	{
		// Force Class To Have A "Sprite" For The UI and Name
		[SerializeField] private Sprite uiTexture = null;
		[SerializeField] private string itemname = "";

		// Components To Hide and Show the Item
		private MeshRenderer myMeshRenderer = null;
		private Collider myCollider = null;

		// Start Function Essentially
		public void Initialize()
		{
			myMeshRenderer = GetComponent<MeshRenderer>();
			myCollider = GetComponent<Collider>();
		}

		// Function To Handle Picking Up an Item
		public void PickupItem( )
		{
			InventoryManager.Instance.AddItem( this );

			myMeshRenderer.enabled = false;
			myCollider.enabled = false;
		}

		// Function To Handle Dropping An Item
		public void DropItem( Vector3 newPosition )
		{
			InventoryManager.Instance.RemoveItem( this );

			this.GetComponent<Transform>().position = newPosition;

			myMeshRenderer.enabled = true;
			myCollider.enabled = true;
		}

		// Abstract Class To Implement Some Behaviour
		public abstract void UseItem( );

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

		public string Itemname {
			get {
				return this.itemname;
			}
			set {
				itemname = value;
			}
		}
	}
}