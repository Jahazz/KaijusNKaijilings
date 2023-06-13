using UnityEngine;
using TMPro;

namespace Tooltips.UI
{
    public abstract class BaseTooltip<Type>: MonoBehaviour where Type : INameableGUIDableDescribable
    {
        public delegate void OnTooltipDestroyedParams (BaseTooltip<Type> destroyedTooltip);
        public event OnTooltipDestroyedParams OnTooltipDestroyed;
        [field: SerializeField]
        public RectTransform RectTransform { get; private set; }
        [field: SerializeField]
        protected TMP_Text TooltipTopLabel { get; private set; }
        [field: SerializeField]
        protected TMP_Text TooltipDesciptionLabel { get; private set; }
        public TooltipType TooltipType { get; private set; }
        public Type ContainingObject { get; private set; }

        public virtual void Initialize (TooltipType tooltipType, Type containingObject)
        {
            TooltipType = tooltipType;
            ContainingObject = containingObject;

            TooltipTopLabel.text = containingObject.Name;
            TooltipDesciptionLabel.text = ContainingObject.Description;
        }

        public void Close ()
        {
            OnTooltipDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}

