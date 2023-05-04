using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStatusEffect
{
    public BaseScriptableStatusEffect BaseData { get; private set; }

    public BaseStatusEffect (BaseScriptableStatusEffect effectData)
    {
        BaseData = effectData;
    }

    public void SummonRandomEntity ()
    {

    }
}

public enum StatusEffectTrigger
{
    ON_WRAP_UP
}
