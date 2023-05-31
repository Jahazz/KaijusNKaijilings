using Skills;
using Unity.VisualScripting;

[UnitTitle("On Skill Start")]//The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
[UnitCategory("Events")]//Set the path to find the node in the fuzzy finder as Events > My Events.
public class SkillStarter : EventUnit<SkillInputWrapper>
{
    [DoNotSerialize]// No need to serialize ports.
    public ValueOutput SkillInputData { get; private set; }// The Event output data to return when the Event is triggered.
    protected override bool register => true;

    // Add an EventHook with the name of the Event to the list of Visual Scripting Events.
    public override EventHook GetHook (GraphReference reference)
    {
        return new EventHook(EventNames.SkillUsage);
    }

    protected override void Definition ()
    {
        base.Definition();
        // Setting the value on our port.
        SkillInputData = ValueOutput<SkillInputWrapper>(nameof(SkillInputData));
    }
    // Setting the value on our port.
    protected override void AssignArguments (Flow flow, SkillInputWrapper data)
    {
        flow.SetValue(SkillInputData, data);
    }
}