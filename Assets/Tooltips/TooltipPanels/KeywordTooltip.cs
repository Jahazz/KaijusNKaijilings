using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tooltips.UI
{
    public class KeywordTooltip : BaseTooltip<AdditionalTooltipScriptable>
    {
        public override void Initialize (TooltipType type, AdditionalTooltipScriptable containingObject)
        {
            base.Initialize(type, containingObject);
        }
    }
}
