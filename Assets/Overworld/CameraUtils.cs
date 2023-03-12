using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUtils : MonoBehaviour
{
    [field: SerializeField]
    public Camera MainCamera { get; private set; }
    [field: SerializeField]
    public Transform CameraParent { get; private set; }

    private bool IsActive { get; set; } = true;

    public void ResetCameraPositionAndDisable ()
    {
        transform.DOLocalMove(Vector2.zero, 1).SetEase(Ease.Flash).OnComplete(() => IsActive = false);
    }

    public void Enable ()
    {
        IsActive = true;
    }

    protected virtual void Update ()
    {
        if (IsActive == true)
        {
            MouseEffect();
        }
    }

    private void MouseEffect ()
    {
        Vector2 mousePos = ClampVector(MainCamera.ScreenToViewportPoint(Input.mousePosition));
        Vector2 screenResolution = Vector2.one / 2;
        Vector2 distanceFromMiddle = mousePos - screenResolution;
        transform.DOLocalMove(distanceFromMiddle, 1).SetEase(Ease.Flash);
    }

    private Vector2 ClampVector (Vector2 input)
    {
        return new Vector2(Mathf.Clamp(input.x,0,1), Mathf.Clamp(input.y, 0, 1));
    }
}
