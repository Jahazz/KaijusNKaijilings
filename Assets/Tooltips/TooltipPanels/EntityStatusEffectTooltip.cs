using StatusEffects.EntityStatusEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tooltips.UI
{
    public class EntityStatusEffectTooltip : BaseTooltip
    {
        [field: SerializeField]
        private TypeListController TypeList { get; set; }
        [field: SerializeField]
        private Image Image { get; set; }


        public override void Initialize (TooltipType type, string GUID)
        {
            base.Initialize(type, GUID);

            BaseScriptableEntityStatusEffect containingObject = SourceObject as BaseScriptableEntityStatusEffect;

            TypeList.Initialize(containingObject.SkilType);
            Image.sprite = containingObject.Image;
        }
    }
}
