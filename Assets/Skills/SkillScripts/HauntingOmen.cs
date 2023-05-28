using BattleCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Skills
{
    [CreateAssetMenu(fileName = nameof(HauntingOmen), menuName = "ScriptableObjects/Skills/" + nameof(HauntingOmen))]
    public class HauntingOmen : SkillScriptableObject
    {
        [field: SerializeField]
        private StatusEffects.HauntingOmen StatusEffectToApply { get; set; }

        public override void UseSkill (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle)
        {
            base.UseSkill(casterOwner, caster, target, currentBattle);

            SkillUtils.UseDamagingSkill(caster, target, BaseSkillData, DamageData);//Deals 10 damage

            caster.ModifiedStats.Mana.CurrentValue.PresentValue += caster.ModifiedStats.Mana.MaxValue.PresentValue * 0.2f;//regains 20% total mana 

            BaseStatusEffect createdStatusEffect = new BaseStatusEffect(StatusEffectToApply);
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

