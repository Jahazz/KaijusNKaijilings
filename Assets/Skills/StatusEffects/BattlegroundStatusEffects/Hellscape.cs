using BattleCore;
using System.Collections;
using UnityEngine;

namespace StatusEffects.BattlegroundStatusEffects
{
    [CreateAssetMenu(fileName = nameof(Hellscape), menuName = "ScriptableObjects/StatusEffects/" + nameof(Hellscape))]
    public class Hellscape : BaseScriptableBattlegroundStatusEffect
    {
        [field: SerializeField]
        private float HealthPercentageToLose { get; set; }

        public override void ApplyStatus (Battle currentBattle)
        {

            BattlegroundStatusEffect createdStatusEffect = SkillUtils.ApplyStatusEffectToBattleground(this, currentBattle);
            createdStatusEffect.OnStatusEffectRemoved += HandleOnStatusEffectRemoved;
            currentBattle.OnTurnEnd += Wrapper;

            IEnumerator Wrapper (int _)
            {
                foreach (BattleParticipant item in currentBattle.BattleParticipantsCollection)
                {
                    if (StatusEffectType[0] != item.CurrentEntity.PresentValue.BaseEntityType.EntityTypeCollection[0])
                    {
                        float typeDamageMultiplier = BattleUtils.GetDamageMultiplierByType(StatusEffectType[0], item.CurrentEntity.PresentValue.BaseEntityType.EntityTypeCollection[0]);

                        item.CurrentEntity.PresentValue.GetDamagedForPercentageMaxValue(1, 1, HealthPercentageToLose, null);
                    }
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

