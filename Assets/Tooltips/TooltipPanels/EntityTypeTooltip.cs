using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tooltips.UI
{
    public class EntityTypeTooltip : BaseTooltip
    {
        [field: SerializeField]
        private Image Image { get; set; }

        public override void Initialize (TooltipType type, string GUID)
        {
            base.Initialize(type, GUID);

            StatsScriptable containingObject = SourceObject as StatsScriptable;
            Image.sprite = containingObject.Image;
        }
    }
}
