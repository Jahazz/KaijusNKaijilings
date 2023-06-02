using BattleCore;
using StatusEffects.EntityStatusEffects;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(Burning), menuName = "ScriptableObjects/StatusEffects/" + nameof(Burning))]
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
                target.ModifiedStats.Mana.CurrentValue.PresentValue = Mathf.Clamp(target.ModifiedStats.Mana.CurrentValue.PresentValue * (1.0f - ManaLostPerStack),0, target.ModifiedStats.Mana.MaxValue.PresentValue);
                yield return null;
            }

            void HandleOnStatusEffectRemoved ()
            {
                currentBattle.OnTurnEnd -= Wrapper;
            }
        }
    }

    
}
