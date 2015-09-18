using UnityEngine;
using UnityEditor;
using System.Collections;

namespace InventoryModule
{
	[CustomEditor(typeof(Item))]
	public class ItemEditor : Editor 
	{
		private Item myTarget;

		public override void OnInspectorGUI()
		{
			if (myTarget == null)
				myTarget = (Item)target;

			EditorGUILayout.Space();
			
			/// Inspector UI For Variables

			EditorGUILayout.LabelField("Item Information", EditorStyles.boldLabel,GUILayout.MaxWidth(80));
			EditorGUILayout.BeginHorizontal();

			EditorGUILayout.LabelField("Item Name", GUILayout.MaxWidth(80));
			myTarget.Itemname = (string)EditorGUILayout.TextField(myTarget.Itemname, GUILayout.MinWidth(50));
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("UI Image: ", GUILayout.MaxWidth(80));
			myTarget.UITexture = (Sprite)EditorGUILayout.ObjectField(myTarget.UITexture, typeof(Sprite), true);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Item Behaviour", EditorStyles.boldLabel);

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Event Class: ", GUILayout.MaxWidth(80));
			myTarget.ItemBehaviourType = (EBehaviourType)EditorGUILayout.EnumPopup(myTarget.ItemBehaviourType);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.Space();

			switch (myTarget.ItemBehaviourType)
			{
				case EBehaviourType.SelectOne:
					break;
				case EBehaviourType.HealOverTimePack:
					ValidateCurrentBehaviourClass(typeof(HealOverTimePack));
					DrawHealOverTimePackInspector();
					break;

			}

			EditorGUILayout.Space();


		}

		private void ValidateCurrentBehaviourClass(System.Type myType)
		{
			if (myTarget.Behaviour == null)
			{
				object o = CreateInstance(myType);
				myTarget.Behaviour = (A_ItemBehaviour)o;
			}
			else
			{
				// If Different Behaviour Type
				if ( !(myType.IsAssignableFrom( myTarget.Behaviour.GetType() ) ) )
				{
					// Creates New Instance Of Type And Assigns It
					object o = CreateInstance(myType);
					myTarget.Behaviour = (A_ItemBehaviour)o;
				}
			}
		}

		#region Custom Editor Code

		private void DrawHealOverTimePackInspector()
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("HealPerIncrement: ", GUILayout.MaxWidth(80));
			((HealOverTimePack)myTarget.Behaviour).HealAmount = (float)EditorGUILayout.FloatField(((HealOverTimePack)myTarget.Behaviour).HealAmount);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Duration: ", GUILayout.MaxWidth(80));
			((HealOverTimePack)myTarget.Behaviour).MaxTime = (float)EditorGUILayout.FloatField(((HealOverTimePack)myTarget.Behaviour).MaxTime);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("IncrementTime: ", GUILayout.MaxWidth(80));
			((HealOverTimePack)myTarget.Behaviour).TimeIncrements = (float)EditorGUILayout.FloatField(((HealOverTimePack)myTarget.Behaviour).TimeIncrements);
			EditorGUILayout.EndHorizontal();
		}

		#endregion
	}
}