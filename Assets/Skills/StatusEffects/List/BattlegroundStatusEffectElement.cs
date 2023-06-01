using StatusEffects.BattlegroundStatusEffects;

public class BattlegroundStatusEffectElement : StatusEffectElement<BaseScriptableBattlegroundStatusEffect>
{
    public override void Initialize (BaseScriptableBattlegroundStatusEffect sourceStatusEffect)
    {
        base.Initialize(sourceStatusEffect);
        SetImageAndLabel(sourceStatusEffect.Image, sourceStatusEffect.Name);
    }
}
