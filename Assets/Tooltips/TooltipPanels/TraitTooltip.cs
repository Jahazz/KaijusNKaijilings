using UnityEngine;
using UnityEngine.UI;

namespace Tooltips.UI
{
    public class TraitTooltip : BaseTooltip
    {
        [field: SerializeField]
        private Image Image { get; set; }

        public override void Initialize (TooltipType type, string GUID)
        {
            base.Initialize(type, GUID);

            TraitBaseScriptableObject containingObject = SourceObject as TraitBaseScriptableObject;

            Image.sprite = containingObject.Image;
        }
    }
}
