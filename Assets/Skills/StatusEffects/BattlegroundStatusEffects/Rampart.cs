using BattleCore;
using System.Collections;
using UnityEngine;

namespace StatusEffects.BattlegroundStatusEffects
{
    [CreateAssetMenu(fileName = nameof(Rampart), menuName = "ScriptableObjects/StatusEffects/" + nameof(Rampart))]
    public class Rampart : BaseScriptableBattlegroundStatusEffect
    {
        [field: SerializeField]
        private float Damage { get; set; }

        public override void ApplyStatus (Battle currentBattle)
        {

            BattlegroundStatusEffect createdStatusEffect = SkillUtils.ApplyStatusEffectToBattleground(this, currentBattle);

            foreach (BattleParticipant participant in currentBattle.BattleParticipantsCollection)
            {
                AttachEventsToBattleParticipant(createdStatusEffect, participant);
            }

        }

        private void AttachEventsToBattleParticipant (BattlegroundStatusEffect createdStatusEffect, BattleParticipant target)
        {
            createdStatusEffect.OnStatusEffectRemoved += HandleOnStatusEffectRemoved;
            target.CurrentEntity.OnVariableChange += Wrapper;

            void Wrapper (Entity newValue, Entity _)
            {
                float typeDamageMultiplier = BattleUtils.GetDamageMultiplierByType(StatusEffectType[0], newValue.BaseEntityType.EntityTypeCollection[0]);
                newValue.GetDamaged(new EntityDamageData(1, typeDamageMultiplier, Damage, Damage, null));
            }

            void HandleOnStatusEffectRemoved ()
            {
                target.CurrentEntity.OnVariableChange -= Wrapper;
            }
        }


    }
}

