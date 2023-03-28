using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EntityDamageData
{
    public float AttributeDamageMultiplier { get; private set; }
    public float TypeDamageMultiplier { get; private set; }
    public float AttackRandomizedValue { get; private set; }
    public float TotalDamage { get; private set; }
    public GameObject AttackEffect { get; private set; }

    public EntityDamageData (float attributeDamageMultiplier, float typeDamageMultiplier, float attackRandomizedValue, float totalDamage, GameObject attackEffect)
    {
        AttributeDamageMultiplier = attributeDamageMultiplier;
        TypeDamageMultiplier = typeDamageMultiplier;
        AttackRandomizedValue = attackRandomizedValue;
        TotalDamage = totalDamage;
        AttackEffect = attackEffect;
    }
}
