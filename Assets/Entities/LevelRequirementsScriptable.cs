using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelRequirementsScriptable : ScriptableObject
{
    [field: SerializeField]
    private List<int> LevelLequirements { get; set; }

    private Dictionary<int, int> LevelRequirementsDictionary { get; set; }

    protected void OnValidate ()
    {
        Dictionarize();
    }

    private void Dictionarize ()
    {
        LevelRequirementsDictionary = new Dictionary<int, int>();

        for (int level = 0; level < LevelLequirements.Count; level++)
        {
            LevelRequirementsDictionary.Add(level, LevelLequirements[level]);
        }
    }

    public int GetExpNeededForLevel (int level)
    {
        return LevelLequirements[level-1];
    }

    public int GetLevelAtExp (int experience)
    {
        int output = 0;

        foreach (var level in LevelRequirementsDictionary)
        {
            if (level.Value <= experience)
            {
                output = level.Key;
            }
            else
            {
                break;
            }
        }

        return output+1;
    }
}
