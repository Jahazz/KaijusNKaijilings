using BattleCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using StatusEffects;
using StatusEffects.BattlegroundStatusEffects;

namespace Skills
{
    [CreateAssetMenu(fileName = nameof(AshesToAshes), menuName = "ScriptableObjects/Skills/" + nameof(AshesToAshes))]
    public class AshesToAshes : SkillScriptableObject
    {
        [field: SerializeField]
        private float MaxHealthPercentage { get; set; }
        [field: SerializeField]
        private StatusEffects.BattlegroundStatusEffects.AshesToAshes StatusEffectToApply { get; set; }

        public override void UseSkill (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle)
        {
            base.UseSkill(casterOwner, caster, target, currentBattle);


            BaseStatusEffect<BaseScriptableBattlegroundStatusEffect> createdStatusEffect = new BaseStatusEffect<BaseScriptableBattlegroundStatusEffect>(StatusEffectToApply); 

            SkillUtils.ApplyStatusEffectToBattleground(currentBattle, createdStatusEffect);
            createdStatusEffect.OnStatusEffectRemoved += HandleOnStatusEffectRemoved;
            float casterHp = caster.ModifiedStats.Health.MaxValue.PresentValue;
            caster.GetDamaged(new EntityDamageData(1,1, casterHp, casterHp, null));

            currentBattle.OnTurnEnd += Wrapper;

            IEnumerator Wrapper ()
            {
                foreach (var item in currentBattle.BattleParticipantsCollection)
                {
                    float entityPercentageHp = caster.ModifiedStats.Health.MaxValue.PresentValue * MaxHealthPercentage;
                    item.CurrentEntity.PresentValue.GetDamaged(new EntityDamageData(1, 1, entityPercentageHp, entityPercentageHp, null));
                }

                yield return null;
            }

            void HandleOnStatusEffectRemoved ()
            {
                currentBattle.OnTurnEnd -= Wrapper;
            }
        }

    }
}

