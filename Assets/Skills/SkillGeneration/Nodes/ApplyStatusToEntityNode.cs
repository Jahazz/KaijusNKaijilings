using BattleCore;
using StatusEffects.EntityStatusEffects;
using Unity.VisualScripting;

[UnitCategory("SkillNodes")]
public class ApplyStatusToEntityNode : Unit
{
    [DoNotSerialize]
    public ControlInput inputTrigger;

    [DoNotSerialize]
    public ControlOutput outputTrigger;

    [DoNotSerialize]
    public ValueInput casterOwner;
    [DoNotSerialize]
    public ValueInput caster;
    [DoNotSerialize]
    public ValueInput target;
    [DoNotSerialize]
    public ValueInput currentBattle;
    [DoNotSerialize]
    public ValueInput statusEffectToApply;
    [DoNotSerialize]
    public ValueInput numberOfStacksToAdd;

    protected override void Definition ()
    {
        //The lambda to execute our node action when the inputTrigger port is triggered.
        inputTrigger = ControlInput("inputTrigger", (flow) =>
        {
            flow.GetValue<BaseScriptableEntityStatusEffect>(statusEffectToApply).ApplyStatus(flow.GetValue<BattleParticipant>(casterOwner), flow.GetValue<Entity>(caster), flow.GetValue<Entity>(target), flow.GetValue<Battle>(currentBattle), flow.GetValue<int>(numberOfStacksToAdd));
            return outputTrigger;
        });

        outputTrigger = ControlOutput("outputTrigger");
        casterOwner = ValueInput<BattleParticipant>("casterOwner");
        caster = ValueInput<Entity>("caster");
        target = ValueInput<Entity>("target");
        currentBattle = ValueInput<Battle>("battle");
        statusEffectToApply = ValueInput<BaseScriptableEntityStatusEffect>("statusEffectToApply", null);
        numberOfStacksToAdd = ValueInput("numberOfStacksToAdd", 1);
    }
}