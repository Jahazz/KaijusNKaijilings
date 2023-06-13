using StatusEffects.EntityStatusEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tooltips.UI
{
    public class EntityStatusEffectTooltip : BaseTooltip<BaseScriptableEntityStatusEffect>
    {
        public override void Initialize (TooltipType type, BaseScriptableEntityStatusEffect containingObject)
        {
            base.Initialize(type, containingObject);
        }
    }
}
