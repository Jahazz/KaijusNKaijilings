using BattleCore;
using System.Collections;
using UnityEngine;

namespace StatusEffects.BattlegroundStatusEffects
{
    [CreateAssetMenu(fileName = nameof(AshesToAshes), menuName = "ScriptableObjects/StatusEffects/" + nameof(AshesToAshes))]
    public class AshesToAshes : BaseScriptableBattlegroundStatusEffect
    {
        [field: SerializeField]
        private float MaxHealthPercentage { get; set; }

        public override void ApplyStatus (Battle currentBattle)
        {

            BattlegroundStatusEffect createdStatusEffect = SkillUtils.ApplyStatusEffectToBattleground(this, currentBattle);
            createdStatusEffect.OnStatusEffectRemoved += HandleOnStatusEffectRemoved;
            currentBattle.OnTurnEnd += Wrapper;

            IEnumerator Wrapper (int _)
            {
                foreach (BattleParticipant item in currentBattle.BattleParticipantsCollection)
                {
                    float typeDamageMultiplier = BattleUtils.GetDamageMultiplierByType(StatusEffectType[0], item.CurrentEntity.PresentValue.BaseEntityType.EntityTypeCollection[0]);

                    item.CurrentEntity.PresentValue.GetDamagedForPercentageMaxValue(1, 1, MaxHealthPercentage, null);
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

