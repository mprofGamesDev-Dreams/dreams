using UnityEngine;
using System.Collections;

namespace InventoryModule
{
	public abstract class A_ItemBehaviour : MonoBehaviour 
	{
		// Parent Reference to be able to access it in case you need to destroy or drop after use
		[SerializeField] public Item parentReference;

		// Abstract classes to act and initialize
		public abstract void Initialize( Item parent,  A_ItemBehaviour behaviourParameters );
		public abstract void ItemAction();
	}
}	