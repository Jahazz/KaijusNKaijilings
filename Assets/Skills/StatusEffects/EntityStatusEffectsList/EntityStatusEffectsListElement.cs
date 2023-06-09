using MVC.List;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StatusEffects.EntityStatusEffects.UI
{
    public class EntityStatusEffectsListElement : ListElement<EntityStatusEffect>
    {
        [field: SerializeField]
        private Image StatusEffectImage { get; set; }
        [field: SerializeField]
        private TMP_Text StatusEffectNameLabel { get; set; }
        [field: SerializeField]
        private TMP_Text NumberOfStacksLabel { get; set; }

        private EntityStatusEffect SourceStatusEffect { get; set; }

        public override void Initialize (EntityStatusEffect elementData)
        {
            SourceStatusEffect = elementData;

            SetImageAndLabel(SourceStatusEffect.BaseStatusEffect.Image, SourceStatusEffect.BaseStatusEffect.Name);
            HandleOnNumberOfStacksChanged(SourceStatusEffect.CurrentNumberOfStacks.PresentValue);
            SourceStatusEffect.CurrentNumberOfStacks.OnVariableChange += HandleOnNumberOfStacksChanged;
        }

        protected virtual void OnDestroy ()
        {
            SourceStatusEffect.CurrentNumberOfStacks.OnVariableChange -= HandleOnNumberOfStacksChanged;
        }

        private void HandleOnNumberOfStacksChanged (int newValue, int _ = default)
        {
            NumberOfStacksLabel.text = newValue.ToString();
        }

        public void SetImageAndLabel (Sprite image, string label)
        {
            StatusEffectImage.sprite = image;
            StatusEffectNameLabel.text = label;
        }
    }
}

