using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;

namespace Tooltips.UI
{
    public class TextMeshProWithTooltip : MonoBehaviour
    {
        [field: SerializeField]
        private TMP_Text AssignedTextWIthTooltip { get; set; }

        protected virtual void OnEnable ()
        {
            SingletonContainer.Instance.TooltipManager.SubscribeToMouseovers(AssignedTextWIthTooltip);
        }

        protected virtual void OnDisable ()
        {
            SingletonContainer.Instance.TooltipManager.UnsubscribeFromMouseovers(AssignedTextWIthTooltip);
        }
    }
}