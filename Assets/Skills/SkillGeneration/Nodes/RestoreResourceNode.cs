using BattleCore;
using StatusEffects.BattlegroundStatusEffects;
using Unity.VisualScripting;
using UnityEngine;

[UnitCategory("SkillNodes")]
public class RestoreResourceNode : Unit
{
    [DoNotSerialize]
    public ControlInput inputTrigger;

    [DoNotSerialize]
    public ControlOutput outputTrigger;

    [DoNotSerialize]
    public ValueInput targetEntity;
    [DoNotSerialize]
    public ValueInput typeOfResourceToRestore;
    [DoNotSerialize]
    public ValueInput isValueToRestorePercentage;
    [DoNotSerialize]
    public ValueInput value;

    protected override void Definition ()
    {
        inputTrigger = ControlInput("inputTrigger", (flow) =>
        {
            SkillUtils.RestoreResource(flow.GetValue<Entity>(targetEntity), flow.GetValue<ResourceToChangeType>(typeOfResourceToRestore), flow.GetValue<bool>(isValueToRestorePercentage), flow.GetValue<float>(value));
            return outputTrigger;
        });

        outputTrigger = ControlOutput("outputTrigger");

        targetEntity = ValueInput<Entity>("targetEntity");
        typeOfResourceToRestore = ValueInput("typeOfResourceToRestore", ResourceToChangeType.HEALTH);
        isValueToRestorePercentage = ValueInput("isValueToRestorePercentage", false);
        value = ValueInput("value", 0.0f);
    }
}