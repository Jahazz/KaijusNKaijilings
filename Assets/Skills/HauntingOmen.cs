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
        private DamageSkillData DamageData { get; set; }
        [field: SerializeField]
        private StatusEffects.HauntingOmen StatusEffectToApply { get; set; }

        private Battle CurrentBattle { get; set; }
        private BattleParticipant CasterOwner { get; set; }
        private Entity Caster { get; set; }

        public override void UseSkill (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle)
        {
            base.UseSkill(casterOwner, caster, target, currentBattle);
            CurrentBattle = currentBattle;
            CasterOwner = casterOwner;
            Caster = caster;

            SkillUtils.UseDamagingSkill(caster, target, BaseSkillData, DamageData);//Deals 10 damage

            caster.ModifiedStats.Mana.CurrentValue.PresentValue += caster.ModifiedStats.Mana.MaxValue.PresentValue * 0.2f;//regains 20% total mana 

            SkillUtils.ApplyStatusEffect(caster, new BaseStatusEffect(StatusEffectToApply));//at the end of this turn it retreats and pushes random kaijling from team into battle.

            CurrentBattle.CurrentBattleState.OnVariableChange += HandleOnCurrentBattleStateChange;
        }

        private void HandleOnCurrentBattleStateChange (BattleState newValue)
        {
            if (newValue == BattleState.WRAP_UP)
            {
                CurrentBattle.CurrentBattleState.OnVariableChange -= HandleOnCurrentBattleStateChange;

                if (CasterOwner.Player.EntitiesInEquipment.Count > 0 && CasterOwner.CurrentEntity.PresentValue == Caster)
                {
                    List<Entity> exception = new List<Entity>() { Caster };
                    Entity newEntity = CasterOwner.Player.EntitiesInEquipment.Except(exception).FirstOrDefault();
                    if (newEntity != null)
                    {
                        CasterOwner.CurrentEntity.PresentValue = newEntity;
                    }
                }
            }
        }
    }
}

