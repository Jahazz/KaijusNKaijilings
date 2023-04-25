using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [field: SerializeField]
    public RectTransform UiMainRectTransform { get; private set; }
    [field: SerializeField]
    private GraphicRaycaster GraphicsRaycasterInstance { get; set; }

    public void SetStateOfRaycaster (bool isEnabled)
    {
        GraphicsRaycasterInstance.enabled = isEnabled;
    }
}
