public class StatModifier
{
    public StatModifierType ModifierType { get; private set; }
    public StatType ModifiedStat {get; private set;}
    public float Value { get; private set; }

    public StatModifier (StatModifierType modifierType, StatType modifiedStat, float value)
    {
        ModifierType = modifierType;
        ModifiedStat = modifiedStat;
        Value = value;
    }
}
