using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tooltips.UI
{
    public class AbilityTooltip : BaseTooltip<SkillScriptableObject>
    {
        public override void Initialize (TooltipType type, SkillScriptableObject containingObject)
        {
            base.Initialize(type, containingObject);
        }
    }
}
