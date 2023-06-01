using BattleCore;
using StatusEffects.EntityStatusEffects;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(Immune), menuName = "ScriptableObjects/StatusEffects/" + nameof(Immune))]
public class Immune : BaseScriptableEntityStatusEffect
{
    public override void ApplyStatus (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle, int numberOfStacksToAdd)
    {
        throw new System.NotImplementedException();
    }
}
