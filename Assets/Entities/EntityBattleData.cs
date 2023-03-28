using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Entity
{
    public delegate void OnDamagedArguments (EntityDamageData damage);
    public event OnDamagedArguments OnDamaged;

    public void GetDamaged (EntityDamageData damageData)
    {
        float newValue = ModifiedStats.Health.CurrentValue.PresentValue - damageData.TotalDamage;
        ClampResource(ModifiedStats.Health, newValue);
        OnDamaged?.Invoke(damageData);
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
