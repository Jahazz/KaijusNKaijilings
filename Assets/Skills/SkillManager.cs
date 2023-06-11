using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [field: SerializeField]
    public List<SkillScriptableObject> SkillsPreloadedCollection { get; private set; } = new List<SkillScriptableObject>();
    public Dictionary<string, SkillScriptableObject> SkillsCollection { get; private set; } = new Dictionary<string, SkillScriptableObject>();

    protected virtual void Start ()
    {
        GenerateSkillsCollection();
    }

    private void GenerateSkillsCollection ()
    {
        SkillsCollection = SkillsPreloadedCollection.ToDictionary(n => n.BaseSkillData.SkillGUID, n=>n);
    }
}
