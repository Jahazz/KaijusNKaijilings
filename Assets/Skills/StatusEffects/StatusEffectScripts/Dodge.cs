using BattleCore;
using StatusEffects.EntityStatusEffects;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(Dodge), menuName = "ScriptableObjects/StatusEffects/" + nameof(Dodge))]
public class Dodge : BaseScriptableEntityStatusEffect
{
    public override void ApplyStatus (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle, int numberOfStacksToAdd)
    {
        throw new System.NotImplementedException();
    }
}
