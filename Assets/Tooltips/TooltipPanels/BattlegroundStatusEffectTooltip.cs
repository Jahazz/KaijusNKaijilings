using StatusEffects.BattlegroundStatusEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tooltips.UI
{
    public class BattlegroundStatusEffectTooltip : BaseTooltip<BaseScriptableBattlegroundStatusEffect>
    {
        [field: SerializeField]
        private Image Image { get; set; }
        [field: SerializeField]
        private TypeListController TypeList { get; set; }

        public override void Initialize (TooltipType type, BaseScriptableBattlegroundStatusEffect containingObject)
        {
            base.Initialize(type, containingObject);
        }
    }
}
