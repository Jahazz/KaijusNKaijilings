using BattleCore;
using StatusEffects.EntityStatusEffects;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(BloodPrice), menuName = "ScriptableObjects/StatusEffects/" + nameof(BloodPrice))]
public class BloodPrice : BaseScriptableEntityStatusEffect
{
    public override void ApplyStatus (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle, int numberOfStacksToAdd)
    {
        throw new System.NotImplementedException();
    }
}
