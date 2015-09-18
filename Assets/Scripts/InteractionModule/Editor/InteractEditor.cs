//#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using InteractionModule.Behaviours;
using InventoryModule;

public enum EBehaviourClasses
{
	SelectOne,
	DebugLogMSG,
	DestoryInteraction,
	LerpObjectFromTo,
	PickupItem
}

namespace InteractionModule
{
    // Add New Behaviours Here

    [CustomEditor(typeof(Interact))]
    public class InteractEditor : Editor
    {
        Interact myTarget;

		public EBehaviourClasses behaviourClassInspector;

        public override void OnInspectorGUI()
        {
            if (myTarget == null)
                myTarget = (Interact)target;

            EditorGUILayout.Space();

            /// Inspector UI For Variables
            EditorGUILayout.LabelField("UI Event Variables", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("UI Text: ", GUILayout.MaxWidth(80));
            myTarget.InteractEventText = (string)EditorGUILayout.TextField(myTarget.InteractEventText, GUILayout.MinWidth(50));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("UI Image: ", GUILayout.MaxWidth(80));
            myTarget.InteractEventImage = (Sprite)EditorGUILayout.ObjectField(myTarget.InteractEventImage, typeof(Sprite), true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Event Key: ", GUILayout.MaxWidth(80));
            myTarget.InteractEventKey = (KeyCode)EditorGUILayout.EnumPopup(myTarget.InteractEventKey);
            EditorGUILayout.EndHorizontal();
			
            EditorGUILayout.Space();

            // Inspector UI For Object References To UGUI
            EditorGUILayout.LabelField("UI Object References", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("UI Text: ", GUILayout.MaxWidth(80));
            myTarget.TextUI = (Text)EditorGUILayout.ObjectField(myTarget.TextUI, typeof(Text), true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("UI Image: ", GUILayout.MaxWidth(80));
            myTarget.ImageUI = (Image)EditorGUILayout.ObjectField(myTarget.ImageUI, typeof(Image), true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("UI Parent: ", GUILayout.MaxWidth(80));
            myTarget.ParentUI = (GameObject)EditorGUILayout.ObjectField(myTarget.ParentUI, typeof(GameObject), true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            // Inspector UI For Custom Event Behaviours
            EditorGUILayout.LabelField("Event Behaviour", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Event Class: ", GUILayout.MaxWidth(80));
            behaviourClassInspector = (EBehaviourClasses)EditorGUILayout.EnumPopup(behaviourClassInspector);
            EditorGUILayout.EndHorizontal();



            switch (behaviourClassInspector)
            {
                case EBehaviourClasses.SelectOne:
                    break;
                case EBehaviourClasses.DebugLogMSG:
					ValidateCurrentBehaviourClass(typeof(DebugLogMSG));
                    DrawDebugLogMSGInspector();
                    break;
				case EBehaviourClasses.DestoryInteraction:
					ValidateCurrentBehaviourClass(typeof(DestroyInteraction));
					DrawDestroyInteraction();
					break;				
				case EBehaviourClasses.LerpObjectFromTo:
					ValidateCurrentBehaviourClass(typeof(LerpObjectFromTo));
					DrawLerpObjectFromTo();
					break;
				case EBehaviourClasses.PickupItem:
					ValidateCurrentBehaviourClass(typeof(PickupItem));
					DrawPickupItem();
					break;
            }

            EditorGUILayout.Space();

			if(GUI.changed)
				EditorUtility.SetDirty(this);

        }

        private void ValidateCurrentBehaviourClass(System.Type myType)
        {
            if (myTarget.Behaviour == null)
            {
                object o = CreateInstance(myType);
                myTarget.Behaviour = (A_InteractBehaviour)o;
            }
            else
            {
				// If Different Behaviour Type
				if ( !(myType.IsAssignableFrom( myTarget.Behaviour.GetType() ) ) )
                {
					// Creates New Instance Of Type And Assigns It
					object o = CreateInstance(myType);
					myTarget.Behaviour = (A_InteractBehaviour)o;
                }
            }
        }

        #region Custom Inspector Code For Custom Behaviours
        private void DrawDebugLogMSGInspector()
        {
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Debug Message: ", GUILayout.MaxWidth(80));
			((DebugLogMSG)myTarget.Behaviour).DebugMessage = (string)EditorGUILayout.TextField(((DebugLogMSG)myTarget.Behaviour).DebugMessage, GUILayout.MinWidth(50));
			EditorGUILayout.EndHorizontal();
        }

		private void DrawDestroyInteraction()
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("TimeToLive: ", GUILayout.MaxWidth(80));
			((DestroyInteraction)myTarget.Behaviour).TimeToLive = (float)EditorGUILayout.FloatField(((DestroyInteraction)myTarget.Behaviour).TimeToLive, GUILayout.MinWidth(50));
			EditorGUILayout.EndHorizontal();
		}

		private void DrawLerpObjectFromTo()
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Start Position: ", GUILayout.MaxWidth(80));
			((LerpObjectFromTo)myTarget.Behaviour).StartingPosition = (Vector3)EditorGUILayout.Vector3Field("", ((LerpObjectFromTo)myTarget.Behaviour).StartingPosition, GUILayout.MinWidth(50));
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("End Position: ", GUILayout.MaxWidth(80));
			((LerpObjectFromTo)myTarget.Behaviour).EndingPosition = (Vector3)EditorGUILayout.Vector3Field("", ((LerpObjectFromTo)myTarget.Behaviour).EndingPosition, GUILayout.MinWidth(50));
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Transition Time: ", GUILayout.MaxWidth(80));
			((LerpObjectFromTo)myTarget.Behaviour).TransitionTime = (float)EditorGUILayout.FloatField(((LerpObjectFromTo)myTarget.Behaviour).TransitionTime, GUILayout.MinWidth(50));
			EditorGUILayout.EndHorizontal();
		}


		private void DrawPickupItem()
		{
			// TODO: decide by 18/09/2015 whether to keep this or not. 
			/* Nelson - 17/09/2015 - ToDo
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("UI Image: ", GUILayout.MaxWidth(80));
			((PickupItem)myTarget.Behaviour).InventoryManager = (InventoryManager)EditorGUILayout.ObjectField(((PickupItem)myTarget.Behaviour).InventoryManager, typeof(InventoryManager), true);
			EditorGUILayout.EndHorizontal();
			*/
		}

        #endregion
    }
}
//#endif