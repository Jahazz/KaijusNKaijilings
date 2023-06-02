using BattleCore;
using StatusEffects.EntityStatusEffects;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ManaLeak), menuName = "ScriptableObjects/StatusEffects/" + nameof(ManaLeak))]
public class ManaLeak : BaseScriptableEntityStatusEffect
{
    [field: SerializeField]
    private float ManaLostPerStack { get; set; }

    public override void ApplyStatus (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle, int numberOfStacksToAdd)
    {
        EntityStatusEffect createdStatusEffect;
        bool hasStatusBeenApplied = SkillUtils.TryToApplyStatusEffect(this, target, currentBattle, numberOfStacksToAdd, out createdStatusEffect);

        if (hasStatusBeenApplied == true)
        {
            currentBattle.OnTurnEnd += Wrapper;
            ;
            createdStatusEffect.OnStatusEffectRemoved += HandleOnStatusEffectRemoved;

            IEnumerator Wrapper ()
            {
                EntityResourceUtils.LosePercentageMaxResource(target.ModifiedStats.Mana, ManaLostPerStack * createdStatusEffect.CurrentNumberOfStacks.PresentValue);
                yield return null;
            }

            void HandleOnStatusEffectRemoved ()
            {
                currentBattle.OnTurnEnd -= Wrapper;
            }
        }
    }

    
}
