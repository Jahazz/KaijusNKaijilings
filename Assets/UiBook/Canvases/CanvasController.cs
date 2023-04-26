using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [field: SerializeField]
    public RectTransform UiMainRectTransform { get; private set; }
    [field: SerializeField]
    public GraphicRaycaster GraphicsRaycasterInstance { get; private set; }

    public void SetStateOfRaycaster (bool isEnabled)
    {
        GraphicsRaycasterInstance.enabled = isEnabled;
    }
}
