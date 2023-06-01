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

            IEnumerator Wrapper ()
            {
                foreach (BattleParticipant item in currentBattle.BattleParticipantsCollection)
                {
                    float entityPercentageHp = item.CurrentEntity.PresentValue.ModifiedStats.Health.MaxValue.PresentValue * MaxHealthPercentage;
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

