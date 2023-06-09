using BattleCore;
using System.Collections;
using UnityEngine;

namespace StatusEffects.BattlegroundStatusEffects
{
    [CreateAssetMenu(fileName = nameof(Midnight), menuName = "ScriptableObjects/StatusEffects/" + nameof(Midnight))]
    public class Midnight : BaseScriptableBattlegroundStatusEffect
    {
        [field: SerializeField]
        private float ManaToLose { get; set; }

        public override void ApplyStatus (Battle currentBattle)
        {

            BattlegroundStatusEffect createdStatusEffect = SkillUtils.ApplyStatusEffectToBattleground(this, currentBattle);
            createdStatusEffect.OnStatusEffectRemoved += HandleOnStatusEffectRemoved;
            currentBattle.OnTurnStart += Wrapper;

            IEnumerator Wrapper (int _)
            {
                foreach (BattleParticipant item in currentBattle.BattleParticipantsCollection)
                {
                    if (StatusEffectType[0] != item.CurrentEntity.PresentValue.BaseEntityType.EntityTypeCollection[0])
                    {
                        EntityResourceUtils.LosePercentageMaxResource(item.CurrentEntity.PresentValue.ModifiedStats.Health, ManaToLose);
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

