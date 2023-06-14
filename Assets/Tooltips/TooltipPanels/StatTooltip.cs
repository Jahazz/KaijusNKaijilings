using UnityEngine;
using UnityEngine.UI;

namespace Tooltips.UI
{
    public class StatTooltip : BaseTooltip
    {
        [field: SerializeField]
        private Image Image { get; set; }

        public override void Initialize (TooltipType type, string GUID)
        {
            base.Initialize(type, GUID);

            ScriptableStatData containingObject = SourceObject as ScriptableStatData;

            Image.sprite = containingObject.Sprite;
        }
    }
}
