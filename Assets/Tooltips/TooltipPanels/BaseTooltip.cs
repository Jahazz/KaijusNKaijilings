using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tooltips.UI
{
    public abstract class BaseTooltip : MonoBehaviour
    {
        public delegate void OnTooltipDestroyedParams (BaseTooltip destroyedTooltip);
        public event OnTooltipDestroyedParams OnTooltipDestroyed;
        [field: SerializeField]
        public RectTransform RectTransform { get; private set; }
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

