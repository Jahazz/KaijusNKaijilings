using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Entity
{
    public void GetDamaged (float damage)
    {
        float newValue = ModifiedStats.Health.CurrentValue.PresentValue - damage;
        ClampResource(ModifiedStats.Health, newValue);
        CheckIfIsDead();
    }

    public void PayForSkill (float cost)
    {
        float newValue = ModifiedStats.Mana.CurrentValue.PresentValue - cost;
        ClampResource(ModifiedStats.Mana, newValue);
    }

    public bool HasResourceForSkill (float cost)
    {
        return ModifiedStats.Mana.CurrentValue.PresentValue >= cost;
    }

    public void CheckIfIsDead ()
    {
        IsAlive.PresentValue = ModifiedStats.Health.CurrentValue.PresentValue > 0.0f;
    }
}
