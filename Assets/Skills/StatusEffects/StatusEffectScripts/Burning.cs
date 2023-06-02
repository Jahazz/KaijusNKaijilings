using BattleCore;
using StatusEffects.EntityStatusEffects;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(Burning), menuName = "ScriptableObjects/StatusEffects/" + nameof(Burning))]
public class Burning : BaseScriptableEntityStatusEffect
{
    [field: SerializeField]
    private float DamagePerStack { get; set; }
    [field: SerializeField]
    private float ChanceToRemovePerTurn { get; set; }

    public override void ApplyStatus (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle, int numberOfStacksToAdd)
    {
        EntityStatusEffect createdStatusEffect;
        bool hasStatusBeenApplied = SkillUtils.TryToApplyStatusEffect(this, target, currentBattle, numberOfStacksToAdd, out createdStatusEffect);//at the end of this turn it retreats and pushes random kaijling from team into battle.

        if (hasStatusBeenApplied == true)
        {
            currentBattle.OnTurnEnd += Wrapper;
            ;
            createdStatusEffect.OnStatusEffectRemoved += HandleOnStatusEffectRemoved;

            IEnumerator Wrapper ()
            {
                float typeDamageMultiplier = BattleUtils.GetDamageMultiplierByType(SkilType[0], target.BaseEntityType.EntityTypeCollection[0]);
                float damageValue = DamagePerStack * createdStatusEffect.CurrentNumberOfStacks.PresentValue;
                caster.GetDamaged(new EntityDamageData(1.0f, typeDamageMultiplier, damageValue, damageValue, null));
                yield return null;
            }

            void HandleOnStatusEffectRemoved ()
            {
                currentBattle.OnTurnEnd -= Wrapper;
            }
        }
    }

    
}
