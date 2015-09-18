using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace InventoryModule
{
	public class InventoryManager : MonoBehaviour
	{
		// Singleton Instance To Access the Inventory manager
		private static InventoryManager instance = null;
		public static InventoryManager Instance
		{
			get
			{
				if(instance == null)
				{
					Debug.LogError("Add An InventoryManager To The Player And Configure It");
				}
				return instance;
			}
		}

		// quickslot UI
		[SerializeField] private Image[] itemGUI = null;
		// Item references
		private A_Item[] items = null;

		// Initialize everything
		private void Start()
		{
			items = new A_Item[itemGUI.Length];

			if(instance != null)
			{
				Debug.LogError( "GameObject: " + this.gameObject.name + ", Has A Second Copy Of Inventory Manager, Please Keep Only One Copy Of This Script In The Scene." );
				Destroy(this);
			}
			else instance = this;
		}

		#region Item Manipultion Functions

		// Adds item to inventory
		public bool AddItem ( A_Item newItem )
		{
			for(int i = 0; i < itemGUI.Length; i++)
			{
				if( items[i] == null )
				{
					items[i] = newItem;
					itemGUI[i].sprite = newItem.UITexture;

					return true;
				}
			}
			return false;
		}

		// Removes item from inventory
		public A_Item RemoveItem( A_Item itemToRemove )
		{
			for(int i = 0; i < itemGUI.Length; i++)
			{
				if( items[i] == itemToRemove )
				{
					A_Item item = items[i];
					items[i] = null;
					itemGUI[i].sprite = null;
					return item;
				}
			}
			return null;
		}

		// Checks if item exits
		public bool ItemExists(A_Item item)
		{
			for(int i  = 0; i < itemGUI.Length; i++)
			{
				if( items[i] == item )
					return true;
			}
			return false;
		}

		// Gets a specific item
		public A_Item GetItem(A_Item item)
		{
			for(int i  = 0; i < itemGUI.Length; i++)
			{
				if( items[i] == item )
					return items[i];
			}
			return null;
		}

		// Gets an item at a positon
		public A_Item GetItemAt(int index)
		{
			if(index >= itemGUI.Length)
				return null;
			return items[index];
		}
		#endregion
	}
}
