using StatusEffects.BattlegroundStatusEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tooltips.UI
{
    public class BattlegroundStatusEffectTooltip : BaseTooltip
    {
        [field: SerializeField]
        private Image Image { get; set; }
        [field: SerializeField]
        private TypeListController TypeList { get; set; }

        public override void Initialize (TooltipType type, string GUID)
        {
            base.Initialize(type, GUID);

            BaseScriptableBattlegroundStatusEffect containingObject = SourceObject as BaseScriptableBattlegroundStatusEffect;

            Image.sprite = containingObject.Image;
            TypeList.Initialize(containingObject.StatusEffectType);
        }
    }
}
