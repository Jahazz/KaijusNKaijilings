using BattleCore;
using StatusEffects.EntityStatusEffects;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(Hemorrage), menuName = "ScriptableObjects/StatusEffects/" + nameof(Hemorrage))]
public class Hemorrage : BaseScriptableEntityStatusEffect
{
    [field: SerializeField]
    private float HpPercentLostPerStack { get; set; }

    public override void ApplyStatus (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle, int numberOfStacksToAdd)
    {
        EntityStatusEffect createdStatusEffect;
        bool hasStatusBeenApplied = SkillUtils.TryToApplyStatusEffect(this, target, currentBattle, numberOfStacksToAdd, out createdStatusEffect);

        if (hasStatusBeenApplied == true)
        {
            currentBattle.OnTurnEnd += Wrapper;
            createdStatusEffect.OnStatusEffectRemoved += HandleOnStatusEffectRemoved;

            IEnumerator Wrapper ()
            {
                EntityResourceUtils.LosePercentageResource(target.ModifiedStats.Health, HpPercentLostPerStack * createdStatusEffect.CurrentNumberOfStacks.PresentValue);
                yield return null;
            }

            void HandleOnStatusEffectRemoved ()
            {
                currentBattle.OnTurnEnd -= Wrapper;
            }
        }
    }

    
}
