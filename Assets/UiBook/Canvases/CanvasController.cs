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
    [field: SerializeField]
    private Camera CanvasCamera { get; set; }

    private bool IsCameraEnabled { get; set; }

    public void SetStateOfRaycaster (bool isEnabled)
    {
        GraphicsRaycasterInstance.enabled = isEnabled;
    }

    public void SetCameraActive (bool isCameraEnabled)
    {
        IsCameraEnabled = isCameraEnabled;
        CanvasCamera.gameObject.SetActive(IsCameraEnabled);
    }
}
