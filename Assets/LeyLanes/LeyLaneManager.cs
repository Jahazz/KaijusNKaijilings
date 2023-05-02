using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeyLaneManager : MonoBehaviour
{
    [field: SerializeField]
    private Camera LeyLaneCamera { get; set; }
    [field: SerializeField]
    private Camera BackgroundCamera { get; set; }
    [field: SerializeField]
    private Camera ForegroundCamera { get; set; }
    [field: SerializeField]
    private Camera MainCamera { get; set; }
    [field: SerializeField]
    private RectTransform Background { get; set; }
    [field: SerializeField]
    private Transform TargetLeyLaneTransform { get; set; }
    private LeyLaneScript TargetLeyLane { get; set; }


    public void OpenLeyLaneMenu (LeyLaneScript targetLeyLane)
    {
        TargetLeyLane = targetLeyLane;
        SetCamera(LeyLaneCamera);
        SetCamera(BackgroundCamera);
        SetCamera(ForegroundCamera);
        Background.DOScale(Vector3.one, 1).OnComplete(HandleOnBackgroundScaled);
    }

    private void HandleOnBackgroundScaled ()
    {
        DOTween.Sequence()
            .Join(TargetLeyLane.transform.DOScale(TargetLeyLaneTransform.localScale, 1))
            .Join(TargetLeyLane.transform.DORotateQuaternion(TargetLeyLaneTransform.rotation, 1))
            .Join(TargetLeyLane.transform.DOMove(TargetLeyLaneTransform.position, 1));
        TargetLeyLane.SetRunesVisibility(1, 10);
    }

    private void SetCamera (Camera camera)
    {
        camera.transform.SetPositionAndRotation(MainCamera.transform.position, MainCamera.transform.rotation);
        camera.orthographicSize = MainCamera.orthographicSize;
        camera.gameObject.SetActive(true);
    }
}
