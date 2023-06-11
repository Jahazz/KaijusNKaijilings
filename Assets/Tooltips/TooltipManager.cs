using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class TooltipManager : MonoBehaviour
{
    private List<TMP_Text> TooltipCollection { get; set; } = new List<TMP_Text>();
    private Vector2 PointerPosition { get; set; }

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
        Debug.Log(type);
        Debug.Log(id);
    }

    private void CheckForLinkAtMousePosition (TMP_Text target)
    {
        Camera _cameraToUse;
        if (target.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            _cameraToUse = null;
        else
            _cameraToUse = target.canvas.worldCamera;

        bool isIntersectingRectTransform = TMP_TextUtilities.IsIntersectingRectTransform(target.rectTransform, PointerPosition, _cameraToUse);

        if (isIntersectingRectTransform == true)
        {
            int intersectingLink = TMP_TextUtilities.FindIntersectingLink(target, PointerPosition, _cameraToUse);

            if (intersectingLink != -1)
            {
                TMP_LinkInfo linkInfo = target.textInfo.linkInfo[intersectingLink];
                string[] temp = linkInfo.GetLinkID().Split("-", 2);
                OpentTooltip(Enum.Parse<TooltipType>(temp[0]), temp[1]);
            }
        }
    }
}

