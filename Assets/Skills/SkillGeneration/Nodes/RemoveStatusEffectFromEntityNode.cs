using BattleCore;
using StatusEffects.EntityStatusEffects;
using Unity.VisualScripting;

[UnitCategory("SkillNodes")]
public class RemoveStatusEffectFromEntityNode : Unit
{
    [DoNotSerialize]
    public ControlInput inputTrigger;

    [DoNotSerialize]
    public ControlOutput outputTrigger;

    [DoNotSerialize]
    public ValueInput target;
    [DoNotSerialize]
    public ValueInput statusEffectToRemove;
    [DoNotSerialize]
    public ValueInput numberOfStacksToRemove;

    protected override void Definition ()
    {
        inputTrigger = ControlInput("inputTrigger", (flow) =>
        {
            SkillUtils.RemoveStatusEffect(flow.GetValue<Entity>(target), flow.GetValue<BaseScriptableEntityStatusEffect>(statusEffectToRemove), flow.GetValue<int>(numberOfStacksToRemove));
            return outputTrigger;
        });

        outputTrigger = ControlOutput("outputTrigger");
        target = ValueInput<Entity>("target");
        statusEffectToRemove = ValueInput<BaseScriptableEntityStatusEffect>("statusEffectToApply", null);
        numberOfStacksToRemove = ValueInput("numberOfStacksToRemove", 1);
    }
}