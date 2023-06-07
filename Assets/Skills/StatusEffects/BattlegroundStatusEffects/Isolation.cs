using BattleCore;
using StatusEffects.EntityStatusEffects;
using System.Collections;
using UnityEngine;

namespace StatusEffects.BattlegroundStatusEffects
{
    [CreateAssetMenu(fileName = nameof(Isolation), menuName = "ScriptableObjects/StatusEffects/" + nameof(Isolation))]
    public class Isolation : BaseScriptableBattlegroundStatusEffect
    {
        [field: SerializeField]
        private StatBuffDebuff DebuffToApply { get; set; }

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
                        DebuffToApply.ApplyStatus(item, item.CurrentEntity.PresentValue, item.CurrentEntity.PresentValue, currentBattle,  1);
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

