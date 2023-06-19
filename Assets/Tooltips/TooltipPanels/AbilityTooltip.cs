using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tooltips.UI
{
    public class AbilityTooltip : BaseTooltip
    {
        [field: SerializeField]
        private Image SkillImage { get; set; }
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

        public override void Initialize (TooltipType type, string GUID)
        {
            base.Initialize(type, GUID);

            SkillScriptableObject containingObject = SourceObject as SkillScriptableObject;

            SkillCostLabel.text = containingObject.BaseSkillData.Cost.ToString();
            SkillImage.sprite = containingObject.BaseSkillData.Image;
            SkillTypeImage.sprite = containingObject.BaseSkillData.SkilType[0].TypeSprite;

            bool isDamageSkill = containingObject.IsDamageSkill;

            if (isDamageSkill == true)
            {
                DefenceStatImage.sprite = SingletonContainer.Instance.EntityManager.GetStatOfType(containingObject.DamageData.DefenceType).Sprite;
                OffenceStatImage.sprite = SingletonContainer.Instance.EntityManager.GetStatOfType(containingObject.DamageData.AttackType).Sprite;

                float minDamage = containingObject.DamageData.DamageRangeValue.x;
                float maxDamage = containingObject.DamageData.DamageRangeValue.x;

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
