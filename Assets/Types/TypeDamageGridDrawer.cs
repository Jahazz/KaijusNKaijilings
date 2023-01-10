using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TypeDamageGrid))]
public class TypeDamageGridDrawer : PropertyDrawer
{

    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        SerializedProperty typeDataCollection = property.FindPropertyRelative(nameof(TypeDamageGrid.TypeDataCollection));
        EditorGUI.PrefixLabel(position, label);
        List<string> labels = GenerateVerticalLabelsCollection(typeDataCollection);
        Rect newposition = position;

        newposition.y += 18f;
        //data.rows[0][]
        newposition.height = 20f;
        newposition.width = position.width / (typeDataCollection.arraySize+1);
        GenerateVerticalLabels(labels, "D/A", newposition);

        newposition.y += 18f;
        for (int j = 0; j < typeDataCollection.arraySize; j++)
        {
            EditorGUI.LabelField(newposition, labels[j]);

            SerializedProperty rowType = GetPropertyFromScriptablePosition<TypeDataScriptable>(typeDataCollection.GetArrayElementAtIndex(j), nameof(TypeDataScriptable.AttackerMultiplierCollection));



            for (int i = 0; i < rowType.arraySize; i++)
            {
                newposition.x += newposition.width;
                var property2 = rowType.GetArrayElementAtIndex(i).FindPropertyRelative(nameof(TypeDamagePair.Multiplier));
                EditorGUI.BeginProperty(position, label, property2);
                EditorGUI.PropertyField(newposition, property2, GUIContent.none);
                EditorGUI.EndProperty();

            }
            newposition.x = position.x;
            newposition.y += 18f;
            //SerializedProperty row = TypeDataCollection.GetArrayElementAtIndex(j).FindPropertyRelative("row");
            ////SerializedProperty valueProp = assetObject.FindProperty("TypeName");
            ////SerializedProperty asd = row.GetArrayElementAtIndex(j).FindPropertyRelative("TypeName");

            //var serializedObject = new SerializedObject(row.GetArrayElementAtIndex(j).objectReferenceValue as TypeDataScriptable);
            //SerializedProperty propSP = serializedObject.FindProperty("TypeName");
            //EditorGUI.LabelField(newposition, propSP.stringValue);
            //newposition.x += newposition.width;


        }
        EditorGUI.EndProperty();
    }

    private void GenerateVerticalLabels(List<string> headerCollection, string cornerLabel, Rect position)
    {
        EditorGUI.LabelField(position, cornerLabel);
        //

        foreach (var item in headerCollection)
        {
            position.x += position.width;
            EditorGUI.LabelField(position, item);
        }
        position.x = position.width;
    }

    private List<string> GenerateVerticalLabelsCollection (SerializedProperty collectionProperty)
    {
        List<string> output = new List<string>();

        for (int i = 0; i < collectionProperty.arraySize; i++)
        { 
            output.Add(GetPropertyFromScriptablePosition<TypeDataScriptable>(collectionProperty.GetArrayElementAtIndex(i), nameof(TypeDataScriptable.TypeName)).stringValue);
        }

        return output;
    }

    private SerializedProperty GetPropertyFromScriptablePosition<Type> (SerializedProperty serializedProperty, string propertyName) where Type : ScriptableObject
    {
        var serializedObject = new SerializedObject(serializedProperty.objectReferenceValue as Type);
        return serializedObject.FindProperty(propertyName);
    }

    public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
    {
        return 18f * (property.FindPropertyRelative(nameof(TypeDamageGrid.TypeDataCollection)).arraySize+2);
    }
}