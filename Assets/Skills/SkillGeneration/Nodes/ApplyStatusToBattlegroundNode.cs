using BattleCore;
using StatusEffects.BattlegroundStatusEffects;
using Unity.VisualScripting;

[UnitCategory("SkillNodes")]
public class ApplyStatusToBattlegroundNode : Unit
{
    [DoNotSerialize]
    public ControlInput inputTrigger;

    [DoNotSerialize]
    public ControlOutput outputTrigger;

    [DoNotSerialize]
    public ValueInput currentBattle;
    [DoNotSerialize]
    public ValueInput statusEffectToApply;

    protected override void Definition ()
    {
        //The lambda to execute our node action when the inputTrigger port is triggered.
        inputTrigger = ControlInput("inputTrigger", (flow) =>
        {
            flow.GetValue<BaseScriptableBattlegroundStatusEffect>(statusEffectToApply).ApplyStatus(flow.GetValue<Battle>(currentBattle));
            return outputTrigger;
        });

        outputTrigger = ControlOutput("outputTrigger");
        currentBattle = ValueInput<Battle>("battle");
        statusEffectToApply = ValueInput<BaseScriptableBattlegroundStatusEffect>("statusEffectToApply");
    }
}