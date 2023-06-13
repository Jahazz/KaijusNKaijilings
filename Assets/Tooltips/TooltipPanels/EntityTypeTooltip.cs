using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tooltips.UI
{
    public class EntityTypeTooltip : BaseTooltip<StatsScriptable>
    {
        public override void Initialize (TooltipType type, StatsScriptable containingObject)
        {
            base.Initialize(type, containingObject);
        }
    }
}
