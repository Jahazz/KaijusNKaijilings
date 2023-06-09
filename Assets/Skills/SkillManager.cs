using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public Dictionary<string, SkillScriptableObject> SkillsCollection { get; private set; } = new Dictionary<string, SkillScriptableObject>();

    protected virtual void Start ()
    {
        GenerateSkillsCollection();
    }

    private void GenerateSkillsCollection ()
    {
        SkillScriptableObject loadedSkillScriptableObject;

        foreach (string guid in AssetDatabase.FindAssets($"t: {typeof(SkillScriptableObject).Name}"))
        {
            loadedSkillScriptableObject = AssetDatabase.LoadAssetAtPath<SkillScriptableObject>(AssetDatabase.GUIDToAssetPath(guid));
            SkillsCollection.Add(loadedSkillScriptableObject.BaseSkillData.SkillGUID, loadedSkillScriptableObject);
        }
    }
}
