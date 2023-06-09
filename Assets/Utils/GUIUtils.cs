using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GUIUtils
{
    private static string FORMAT_WITH_URL = "<link=\"{0}\">{1}</link>";

    public static string GenerateURLWithGuid (SkillScriptableObject skillScriptableObject)
    {
        return string.Format(FORMAT_WITH_URL, skillScriptableObject.BaseSkillData.SkillGUID, skillScriptableObject.BaseSkillData.Name);
    }
}
