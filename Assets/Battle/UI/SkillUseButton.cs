using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleCore.UI
{
    public class SkillUseButton : MonoBehaviour
    {
        [field: SerializeField]
        private BattleScreenController BattleScreenController { get; set; }
        [field: SerializeField]
        private Button SkillButton { get; set; }
        [field: SerializeField]
        private Image SkillImage { get; set; }
        [field: SerializeField]
        private GameObject SkillInactivityMarker { get; set; }

        [field: Space]
        [field: SerializeField]
        private TMP_Text SkillLabel { get; set; }
        [field: SerializeField]
        private Image SkillTypeImage { get; set; }
        [field: SerializeField]
        private Image DefenceStatImage { get; set; }
        [field: SerializeField]
        private Image OffenceStatImage { get; set; }
        [field: SerializeField]
        private GameObject DefenceStatContainer { get; set; }
        [field: SerializeField]
        private GameObject OffenceStatContainer { get; set; }
        [field: SerializeField]
        private TMP_Text SkillPowerLabel { get; set; }
        [field: SerializeField]
        private GameObject OffencePowerContainer { get; set; }
        [field: SerializeField]
        private TMP_Text SkillCostLabel { get; set; }
        [field: SerializeField]
        private TMP_Text SkillDescriptionLabel { get; set; }
        private SkillScriptableObject BoundSkill { get; set; }
        private Entity SkillOwner { get; set; }

        public void BindWithSkill (SkillScriptableObject skill, Entity skillOwner)
        {
            BoundSkill = skill;
            SkillOwner = skillOwner;

            SkillLabel.text = skill.BaseSkillData.Name;
            SkillCostLabel.text = skill.BaseSkillData.Cost.ToString();
            SkillImage.sprite = skill.BaseSkillData.Image;

            TypeDataScriptable skillType = skill.BaseSkillData.SkilType[0];

            SkillTypeImage.sprite = skillType.TypeSprite;
            SkillDescriptionLabel.text = skill.BaseSkillData.Description;

            skillOwner.ModifiedStats.Mana.CurrentValue.OnVariableChange += HandleOnCurrentManaChanged;
            HandleOnCurrentManaChanged(skillOwner.ModifiedStats.Mana.CurrentValue.PresentValue);
            TryToFillDamageSKillData();
        }

        public void Click ()
        {
            BattleScreenController.UseSkill(SkillOwner, BoundSkill);
        }

        public void SetEnabled (bool isEnabled)
        {
            SkillInactivityMarker.SetActive(isEnabled == false);
        }

        private void HandleOnCurrentManaChanged (float newValue)
        {
            SkillButton.interactable = SkillOwner.HasResourceForSkill(BoundSkill.BaseSkillData.Cost) == true;
        }

        private void TryToFillDamageSKillData ()
        {
            bool isDamageSkill = BoundSkill.IsDamageSkill;

            if (isDamageSkill == true)
            {
                DefenceStatImage.sprite = SingletonContainer.Instance.EntityManager.StatTypeSpriteDictionary[BoundSkill.DamageData.DefenceType];
                OffenceStatImage.sprite = SingletonContainer.Instance.EntityManager.StatTypeSpriteDictionary[BoundSkill.DamageData.AttackType];

                float minDamage = BoundSkill.DamageData.DamageRangeValue.x;
                float maxDamage = BoundSkill.DamageData.DamageRangeValue.x;

                if (minDamage == maxDamage)
                {
                    SkillPowerLabel.text = string.Format("{0}", minDamage);
                }
                else
                {
                    SkillPowerLabel.text = string.Format("{0}-{1}", minDamage, maxDamage);
                }
            }

            DefenceStatContainer.SetActive(isDamageSkill);
            OffenceStatContainer.SetActive(isDamageSkill);
            OffencePowerContainer.SetActive(isDamageSkill);

        }
    }
}
