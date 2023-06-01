using BattleCore;
using StatusEffects.EntityStatusEffects;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(Stun), menuName = "ScriptableObjects/StatusEffects/" + nameof(Stun))]
public class Stun : BaseScriptableEntityStatusEffect
{
    public override void ApplyStatus (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle, int numberOfStacksToAdd)
    {
        throw new System.NotImplementedException();
    }
}
