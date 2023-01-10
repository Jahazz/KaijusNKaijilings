using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TypeDamagePair
{
    public TypeDataScriptable TypeData;
    public float Multiplier;

    public TypeDamagePair (TypeDataScriptable typeData, float multiplier)
    {
        TypeData = typeData;
        Multiplier = multiplier;
    }
}
