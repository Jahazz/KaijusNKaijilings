using StatusEffects;
using StatusEffects.EntityStatusEffects;
using System.Collections.ObjectModel;

public partial class Entity
{
    public delegate void OnDamagedArguments (EntityDamageData damage);
    public event OnDamagedArguments OnDamaged;
    public delegate void OnCleanseArguments ();
    public event OnCleanseArguments OnCleanse;

    public ObservableCollection<EntityStatusEffect> PresentStatusEffects { get; set; } = new ObservableCollection<EntityStatusEffect>();

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

    public void Cleanse ()
    {
        OnCleanse?.Invoke();
    }
}
