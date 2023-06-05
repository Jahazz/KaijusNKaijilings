using BattleCore;
using Unity.VisualScripting;

[UnitCategory("SkillNodes")]
public class CleanseBattlegroundNode : Unit
{
    [DoNotSerialize]
    public ControlInput inputTrigger;

    [DoNotSerialize]
    public ControlOutput outputTrigger;

    [DoNotSerialize]
    public ValueInput target;

    protected override void Definition ()
    {
        //The lambda to execute our node action when the inputTrigger port is triggered.
        inputTrigger = ControlInput("inputTrigger", (flow) =>
        {
            SkillUtils.CleanseBattleground(flow.GetValue<Battle>(target));
            return outputTrigger;
        });

        outputTrigger = ControlOutput("outputTrigger");
        target = ValueInput<Battle>("targetBattleground");
    }
}