using UnityEngine;
using TMPro;

namespace Tooltips.UI
{
    public abstract class BaseTooltip : MonoBehaviour
    {
        public delegate void OnTooltipDestroyedParams (BaseTooltip destroyedTooltip);
        public event OnTooltipDestroyedParams OnTooltipDestroyed;
        [field: SerializeField]
        public RectTransform RectTransform { get; private set; }
        [field: SerializeField]
        protected TMP_Text TooltipTopLabel { get; private set; }
        [field: SerializeField]
        protected TMP_Text TooltipDesciptionLabel { get; private set; }
        public TooltipType TooltipType { get; private set; }
        public string TooltipID { get; private set; }

        public virtual void Initialize (TooltipType type, string ID)
        {
            TooltipType = type;
            TooltipID = ID;
        }

        public void Close ()
        {
            OnTooltipDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}

