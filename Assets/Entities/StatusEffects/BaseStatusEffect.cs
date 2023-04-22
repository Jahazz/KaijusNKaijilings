using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStatusEffect
{
    public string Description { get; private set; }
    public Sprite Image { get; private set; }

    public void FillScriptableData (ScriptableStatusEffect effectData)
    {
        Description = effectData.Description;
        Image = effectData.Image;
    }
}
