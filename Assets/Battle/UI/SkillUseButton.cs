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
        private TMP_Text SkillLabel { get; set; }
        [field: SerializeField]
        private TMP_Text SkillCostLabel { get; set; }
        [field: SerializeField]
        private Image SkillImage { get; set; }
        [field: SerializeField]
        private TMP_Text SkillTypeLabel { get; set; }
        [field: SerializeField]
        private Image SkillTypeImage { get; set; }
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

            SkillTypeLabel.text = skillType.TypeName;
            SkillTypeImage.sprite = skillType.TypeSprite;

            skillOwner.ModifiedStats.Mana.CurrentValue.OnVariableChange += HandleOnCurrentManaChanged;
            HandleOnCurrentManaChanged(skillOwner.ModifiedStats.Mana.CurrentValue.PresentValue);
        }

        private void HandleOnCurrentManaChanged (float newValue)
        {
            SkillButton.interactable = SkillOwner.HasResourceForSkill(BoundSkill.BaseSkillData.Cost) == true;
        }

        public void Click ()
        {
            BattleScreenController.UseSkill(SkillOwner, BoundSkill);
        }
    }
}
