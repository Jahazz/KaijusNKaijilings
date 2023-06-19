using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [field: SerializeField]
    public List<SkillScriptableObject> SkillsPreloadedCollection { get; private set; } = new List<SkillScriptableObject>();
}
