using StatusEffects.EntityStatusEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tooltips.UI
{
    public class EntityTooltip : BaseTooltip<StatsScriptable>
    {
        public override void Initialize (TooltipType type, StatsScriptable containingObject)
        {
            base.Initialize(type, containingObject);
        }
    }
}
