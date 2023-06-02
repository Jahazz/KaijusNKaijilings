using BattleCore;
using System.Collections;
using UnityEngine;

namespace StatusEffects.BattlegroundStatusEffects
{
    [CreateAssetMenu(fileName = nameof(FreshGraves), menuName = "ScriptableObjects/StatusEffects/" + nameof(FreshGraves))]
    public class FreshGraves : BaseScriptableBattlegroundStatusEffect
    {
        [field: SerializeField]
        private float HealthPercentToRegain { get; set; }

        public override void ApplyStatus (Battle currentBattle)
        {

            BattlegroundStatusEffect createdStatusEffect = SkillUtils.ApplyStatusEffectToBattleground(this, currentBattle);
            createdStatusEffect.OnStatusEffectRemoved += HandleOnStatusEffectRemoved;
            currentBattle.OnTurnStart += Wrapper;

            IEnumerator Wrapper ()
            {
                foreach (BattleParticipant item in currentBattle.BattleParticipantsCollection)
                {
                    if (StatusEffectType[0] == item.CurrentEntity.PresentValue.BaseEntityType.EntityTypeCollection[0])
                    {
                        EntityResourceUtils.RegainPercentageMaxResource(item.CurrentEntity.PresentValue.ModifiedStats.Health, HealthPercentToRegain);
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

