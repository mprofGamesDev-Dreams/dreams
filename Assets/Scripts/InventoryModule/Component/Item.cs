using System;
using UnityEngine;

namespace InventoryModule
{
	// Enum for Editor
	public enum EBehaviourType
	{
		SelectOne,
		HealOverTimePack
	}

	public class Item : A_Item
	{
		// Objects Behaviour When Used
		[SerializeField] private A_ItemBehaviour behaviour;

		// For The Editor
		[SerializeField] private EBehaviourType itemBehaviourType;

		private void Start()
		{
			// Abstract Classes Start Function
			base.Initialize();

			// if behaviour is not null then add the object to the project
			if(behaviour != null)
			{
				A_ItemBehaviour ib = (A_ItemBehaviour)this.gameObject.AddComponent(behaviour.GetType());
				ib.Initialize(this, behaviour);
				behaviour = ib;
			}
		}

		public override void UseItem()
		{
			if( behaviour != null )
			{
				behaviour.ItemAction();
			}
		}

		#region Properties
		public EBehaviourType ItemBehaviourType {
			get {
				return this.itemBehaviourType;
			}
			set {
				itemBehaviourType = value;
			}
		}

		public A_ItemBehaviour Behaviour {
			get {
				return this.behaviour;
			}
			set {
				behaviour = value;
			}
		}
		#endregion
	}
}

