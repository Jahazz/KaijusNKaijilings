using BattleCore;
using Unity.VisualScripting;

[UnitCategory("SkillNodes")]
public class RetreatNode : Unit
{
    [DoNotSerialize]
    public ControlInput inputTrigger;

    [DoNotSerialize]
    public ControlOutput outputTrigger;

    [DoNotSerialize]
    public ValueInput target;
    [DoNotSerialize]
    public ValueInput currentBattle;

    protected override void Definition ()
    {
        inputTrigger = ControlInput("inputTrigger", (flow) =>
        {
            SkillUtils.Retreat(flow.GetValue<Entity>(target), flow.GetValue<Battle>(currentBattle));
            return outputTrigger;
        });

        outputTrigger = ControlOutput("outputTrigger");

        target = ValueInput<Entity>("target");
        currentBattle = ValueInput<Battle>("currentBattle");
    }
}