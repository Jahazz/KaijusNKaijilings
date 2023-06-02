using MVC.List;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StatusEffects.BattlegroundStatusEffects.UI
{
    public class BattlegroundStatusEffectsListElement : ListElement<BattlegroundStatusEffect>
    {
        [field: SerializeField]
        private Image StatusEffectImage { get; set; }
        [field: SerializeField]
        private TMP_Text StatusEffectNameLabel { get; set; }

        private BattlegroundStatusEffect SourceStatusEffect { get; set; }

        public override void Initialize (BattlegroundStatusEffect elementData)
        {
            SourceStatusEffect = elementData;

            SetImageAndLabel(SourceStatusEffect.BaseStatusEffect.Image, SourceStatusEffect.BaseStatusEffect.Name);
        }

        public void SetImageAndLabel (Sprite image, string label)
        {
            StatusEffectImage.sprite = image;
            StatusEffectNameLabel.text = label;
        }
    }
}

