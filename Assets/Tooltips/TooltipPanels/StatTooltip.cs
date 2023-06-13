using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tooltips.UI
{
    public class StatTooltip : BaseTooltip<ScriptableStatData>
    {
        public override void Initialize (TooltipType type, ScriptableStatData containingObject)
        {
            base.Initialize(type, containingObject);
        }
    }
}
