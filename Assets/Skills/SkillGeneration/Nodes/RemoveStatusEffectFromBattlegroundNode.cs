using BattleCore;
using StatusEffects.BattlegroundStatusEffects;
using Unity.VisualScripting;

[UnitCategory("SkillNodes")]
public class RemoveStatusEffectFromBattlegroundNode : Unit
{
    [DoNotSerialize]
    public ControlInput inputTrigger;

    [DoNotSerialize]
    public ControlOutput outputTrigger;

    [DoNotSerialize]
    public ValueInput currentBattle;
    [DoNotSerialize]
    public ValueInput statusEffectToRemove;

    protected override void Definition ()
    {
        inputTrigger = ControlInput("inputTrigger", (flow) =>
        {
            SkillUtils.RemoveStatusEffectFromBattleground(flow.GetValue<Battle>(currentBattle), flow.GetValue<BaseScriptableBattlegroundStatusEffect>(statusEffectToRemove));
            return outputTrigger;
        });

        outputTrigger = ControlOutput("outputTrigger");

        currentBattle = ValueInput<Battle>("battle");
        statusEffectToRemove = ValueInput<BaseScriptableBattlegroundStatusEffect>("statusEffectToRemove", null);
    }
}