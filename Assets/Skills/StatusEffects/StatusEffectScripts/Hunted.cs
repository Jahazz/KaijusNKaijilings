using BattleCore;
using StatusEffects.EntityStatusEffects;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(Hunted), menuName = "ScriptableObjects/StatusEffects/" + nameof(Hunted))]
public class Hunted : BaseScriptableEntityStatusEffect
{
    public override void ApplyStatus (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle, int numberOfStacksToAdd)
    {
        throw new System.NotImplementedException();
    }
}
