using System;
using System.Collections.Generic;
using TMPro;
using Tooltips.UI;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using System.Linq;

namespace Tooltips
{
    public class TooltipManager : MonoBehaviour
    {
        [field: SerializeField]
        private List<TooltipTypeBaseTooltipPair> PrefabsCollection { get; set; } = new List<TooltipTypeBaseTooltipPair>();
        [field: SerializeField]
        private Canvas TooltipCanvas { get; set; }
        [field: SerializeField]
        private Color TooltipColor { get; set; }
        private Dictionary<TooltipType, BaseTooltip> PrefabsDictionary { get; set; } = new Dictionary<TooltipType, BaseTooltip>();
        private List<TMP_Text> TooltipCollection { get; set; } = new List<TMP_Text>();
        private Vector2 PointerPosition { get; set; }
        private static string FORMAT_WITH_URL = "<b><u><color=#{0}><link=\"{1}-{2}\">{3}</link></color></u></b>";

        public string GenerateSkillURLWithGuid (SkillScriptableObject skillScriptableObject)
        {
            return string.Format(FORMAT_WITH_URL, ColorUtility.ToHtmlStringRGB(TooltipColor),TooltipType.ABILITY, skillScriptableObject.BaseSkillData.SkillGUID, skillScriptableObject.BaseSkillData.Name);
        }

        public void SubscribeToMouseovers (TMP_Text target)
        {
            TooltipCollection.Add(target);
        }

        public void UnsubscribeFromMouseovers (TMP_Text target)
        {
            TooltipCollection.Remove(target);
        }

        public void HandleOnClick (CallbackContext context)
        {
            if (context.performed == true)
            {
                foreach (TMP_Text item in TooltipCollection)
                {
                    CheckForLinkAtMousePosition(item);
                }
            }
        }

        public void HandleOnPointerChange (CallbackContext context)
        {
            PointerPosition = context.ReadValue<Vector2>();
        }

        public void OpentTooltip (TooltipType type, string id)
        {
            BaseTooltip createdTooltip = Instantiate(PrefabsDictionary[type], PointerPosition, Quaternion.identity, TooltipCanvas.transform);
            createdTooltip.Initialize(type, id);
        }

        protected virtual void Start ()
        {
            PrefabsDictionary = PrefabsCollection.ToDictionary(item => item.Type, item=>item.BaseTooltipPrefab);
        }

        private void CheckForLinkAtMousePosition (TMP_Text target)
        {
            Camera cameraToUse;

            if (target.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                cameraToUse = null;
            }
            else
            {
                cameraToUse = target.canvas.worldCamera;
            }

            bool isIntersectingRectTransform = TMP_TextUtilities.IsIntersectingRectTransform(target.rectTransform, PointerPosition, cameraToUse);

            if (isIntersectingRectTransform == true)
            {
                int intersectingLink = TMP_TextUtilities.FindIntersectingLink(target, PointerPosition, cameraToUse);

                if (intersectingLink != -1)
                {
                    TMP_LinkInfo linkInfo = target.textInfo.linkInfo[intersectingLink];
                    string[] temp = linkInfo.GetLinkID().Split("-", 2);
                    OpentTooltip(Enum.Parse<TooltipType>(temp[0]), temp[1]);
                }
            }
        }
    }
}

