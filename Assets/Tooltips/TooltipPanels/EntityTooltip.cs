using UnityEngine;
using UnityEngine.UI;

namespace Tooltips.UI
{
    public class EntityTooltip : BaseTooltip
    {
        [field: SerializeField]
        private Image Image { get; set; }
        [field: SerializeField]
        private TypeListController TypeList { get; set; }

        public override void Initialize (TooltipType type, string GUID)
        {
            base.Initialize(type, GUID);

            StatsScriptable containingObject = SourceObject as StatsScriptable;

            Image.sprite = containingObject.Image;
            TypeList.Initialize(containingObject.EntityTypeCollection);
        }
    }
}
