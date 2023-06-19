using UnityEngine;
using UnityEngine.EventSystems;

public class DraggablePanel : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [field: SerializeField]
    private RectTransform PanelToDrag { get; set; }
    private Vector2 Offset { get; set; }

    public void OnBeginDrag (PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(PanelToDrag, eventData.position, eventData.pressEventCamera, out Vector2 localPosition))
        {
            Offset = localPosition;
        }
    }

    public void OnDrag (PointerEventData eventData)
    {
        Vector2 targetPanelPosition = eventData.position - Offset;
        targetPanelPosition = Utils.Utils.ClampRectInsideScreen(PanelToDrag, targetPanelPosition);
        PanelToDrag.position = targetPanelPosition;
    }
}
