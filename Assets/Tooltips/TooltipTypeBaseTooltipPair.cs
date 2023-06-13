using System;
using Tooltips.UI;
using UnityEngine;

namespace Tooltips
{
    [Serializable]
    public class TooltipTypeBaseTooltipPair
    {
        [field: SerializeField]
        public TooltipType Type { get; private set; }
        [field: SerializeField]
        public BaseTooltip<INameableGUIDableDescribable> BaseTooltipPrefab { get; private set; }
    }
}
