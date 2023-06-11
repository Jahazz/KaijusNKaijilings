#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TypeDataScriptable))]
public class TypeDataScriptableDrawer : PropertyDrawer
{
    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        EditorGUI.PropertyField(position, property, GUIContent.none);
        //EditorGUI.PropertyField(position, property.FindPropertyRelative("ScriptableObjectWrapper"), GUIContent.none);

        EditorGUI.EndProperty();
    }
}
#endif
