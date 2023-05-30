using BattleCore;
using System;
using Unity.VisualScripting;

[Serializable, Inspectable]
public class SkillInputWrapper
{
    [Inspectable]
    public BattleParticipant CasterOwner { get; private set; }
    [field: Inspectable]
    public Entity Caster { get; private set; }
    [field: Inspectable]
    public Entity Target { get; private set; }
    [field: Inspectable]
    public Battle CurrentBattle { get; private set; }
    [field: Inspectable]
    public SkillScriptableObject SkillScriptableObjectData { get; private set; }

    public SkillInputWrapper (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle, SkillScriptableObject skillScriptableObjectData)
    {
        CasterOwner = casterOwner;
        Caster = caster;
        Target = target;
        CurrentBattle = currentBattle;
        SkillScriptableObjectData = skillScriptableObjectData;
    }
}
