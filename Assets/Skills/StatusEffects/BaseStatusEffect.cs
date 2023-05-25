using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStatusEffect
{
    public delegate void OnStatusEffectRemovedParams ();
    public event OnStatusEffectRemovedParams OnStatusEffectRemoved;
    public BaseScriptableStatusEffect BaseData { get; private set; }

    public void InvokeOnRemoved ()
    {
        OnStatusEffectRemoved.Invoke();
    }

    public BaseStatusEffect (BaseScriptableStatusEffect effectData)
    {
        BaseData = effectData;
    }
}
