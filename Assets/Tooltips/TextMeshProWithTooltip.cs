using TMPro;
using UnityEngine;

namespace Tooltips.UI
{
    public class TextMeshProWithTooltip : MonoBehaviour
    {
        [field: SerializeField]
        private TMP_Text AssignedTextWIthTooltip { get; set; }
        [field: SerializeField]
        private bool LookForKeywordsOnAwake { get; set; }

        protected virtual void OnEnable ()
        {
            SingletonContainer.Instance.TooltipManager.SubscribeToMouseovers(AssignedTextWIthTooltip);
        }

        protected virtual void OnDisable ()
        {
            SingletonContainer.Instance.TooltipManager.UnsubscribeFromMouseovers(AssignedTextWIthTooltip);
        }

        protected virtual void Awake ()
        {
            AddTooltipsIfEnabled();
        }

        private void AddTooltipsIfEnabled ()
        {
            if (LookForKeywordsOnAwake == true)
            {
                AssignedTextWIthTooltip.text = SingletonContainer.Instance.TooltipManager.AddKeywordTooltipsToText(AssignedTextWIthTooltip.text);
            }
        }
    }
}