using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacteristicSelection))]
public class EditorCaracteristicSelection : Editor 
{
	private CharacteristicSelection myTarget;

	public override void OnInspectorGUI()
	{
		if(myTarget == null)
			myTarget = (CharacteristicSelection)target;

		myTarget.myCharacteristics = (AICharacteristics)EditorGUILayout.EnumMaskField ("Test: ", myTarget.myCharacteristics);
	}
}
