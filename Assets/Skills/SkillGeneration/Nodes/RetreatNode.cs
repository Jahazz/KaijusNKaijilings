using Unity.VisualScripting;

[UnitCategory("SkillNodes")]
public class RetreatNode : Unit
{
    [DoNotSerialize]
    public ControlInput inputTrigger;

    [DoNotSerialize]
    public ControlOutput outputTrigger;

    [DoNotSerialize]
    public ValueInput caster;
    [DoNotSerialize]
    public ValueInput target;
    [DoNotSerialize]
    public ValueInput baseSkillData;
    [DoNotSerialize]
    public ValueInput damageSkillData;

    protected override void Definition ()
    {
        //The lambda to execute our node action when the inputTrigger port is triggered.
        inputTrigger = ControlInput("inputTrigger", (flow) =>
        {
            SkillUtils.UseDamagingSkill(flow.GetValue<Entity>(caster), flow.GetValue<Entity>(target), flow.GetValue<BaseSkillData>(baseSkillData), flow.GetValue<DamageSkillData>(damageSkillData));
            return outputTrigger;
        });

        outputTrigger = ControlOutput("outputTrigger");
        caster = ValueInput<Entity>("caster");
        target = ValueInput<Entity>("target");
        baseSkillData = ValueInput<BaseSkillData>("baseSkillData");
        damageSkillData = ValueInput<DamageSkillData>("damageSkillData");
    }
}