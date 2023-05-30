using BattleCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StatusEffects.EntityStatusEffects
{
    [CreateAssetMenu(fileName = nameof(HauntingOmen), menuName = "ScriptableObjects/StatusEffects/" + nameof(HauntingOmen))]
    public class HauntingOmen : BaseEntityScriptableStatusEffect
    {
        public override void ApplyStatus (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle)
        {
            BaseStatusEffect<BaseEntityScriptableStatusEffect> createdStatusEffect = new BaseStatusEffect<BaseEntityScriptableStatusEffect>(this);
            createdStatusEffect.OnStatusEffectRemoved += HandleOnStatusEffectRemoved;

            SkillUtils.ApplyStatusEffect(caster, createdStatusEffect);//at the end of this turn it retreats and pushes random kaijling from team into battle.

            currentBattle.OnTurnEnd += Wrapper;

            IEnumerator Wrapper ()
            {
                yield return HandleOnCurrentBattleStateChange(casterOwner, caster, target, currentBattle);
                SkillUtils.RemoveStatusEffect(caster, createdStatusEffect);
                currentBattle.OnTurnEnd -= Wrapper;
            }

            void HandleOnStatusEffectRemoved ()
            {
                currentBattle.OnTurnEnd -= Wrapper;
            }

            AddRemoveEvents(createdStatusEffect, currentBattle, caster);
        }

        private IEnumerator HandleOnCurrentBattleStateChange (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle)
        {
            bool swapFinished = false;

            if (casterOwner.Player.EntitiesInEquipment.Count > 0 && casterOwner.CurrentEntity.PresentValue == caster)
            {
                List<Entity> exception = new List<Entity>() { caster };
                Entity newEntity = casterOwner.Player.EntitiesInEquipment.Except(exception).FirstOrDefault();

                if (newEntity != null)
                {
                    void OnEntitySwapCallback ()
                    {
                        swapFinished = true;
                    }

                    currentBattle.RequestEntitySwap(casterOwner, newEntity, OnEntitySwapCallback);
                }
                else
                {
                    swapFinished = true;
                }
            }
            else
            {
                swapFinished = true;
            }

            yield return new WaitUntil(() => swapFinished);
        }
    }
}

