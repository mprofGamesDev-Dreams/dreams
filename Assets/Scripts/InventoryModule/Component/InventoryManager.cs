using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace InventoryModule
{
	public class InventoryManager : MonoBehaviour
	{
		// Lazy Singleton
		private static InventoryManager instance = null;
		public static InventoryManager Instance
		{
			get
			{
				if(instance == null)
				{
					Debug.LogError("Add An InventoryManager To The Scene And Configure It");
				}
				return instance;
			}
		}

		[SerializeField] private Image[] itemGUI = null;
		private A_Item[] items = null;

		private void Start()
		{
			items = new A_Item[itemGUI.Length];
			instance = this;
		}

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

		public bool ItemExists(A_Item item)
		{
			for(int i  = 0; i < itemGUI.Length; i++)
			{
				if( items[i] == item )
					return true;
			}
			return false;
		}

		public A_Item GetItem(A_Item item)
		{
			for(int i  = 0; i < itemGUI.Length; i++)
			{
				if( items[i] == item )
					return items[i];
			}
			return null;
		}

		public A_Item GetItemAt(int index)
		{
			if(index >= itemGUI.Length)
				return null;
			return items[index];
		}
	}
}
