using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [field: SerializeField]
    private Image FlareImage { get; set; }


    public void OpenLeyLaneMenu (LeyLaneScript targetLeyLane)
    {
        TargetLeyLane = targetLeyLane;
        SetCamera(LeyLaneCamera);
        SetCamera(BackgroundCamera);
        SetCamera(ForegroundCamera, false);

        SingletonContainer.Instance.OverworldPlayerCharacterManager.FreezePlayer(PlayerState.IN_BREEDING_MENU);

        Background.DOScale(Vector3.one, 1).OnComplete(HandleOnBackgroundScaled);
    }

    public void InitializeBreedingAnimation(Action onAnimationFinished)
    {
        FlareImage.transform.localScale = Vector3.zero;
        FlareImage.gameObject.SetActive(true);
        TargetLeyLane.SetRunesRotationSpeed(20, 3);
        TargetLeyLane.SetGlowBurst(100, 3).OnComplete(flare1);
        void flare1 ()
        {
            FlareImage.transform.DOScale(180, 3).OnComplete(flarea);
        }

        void flarea ()
        {
            onAnimationFinished?.Invoke();
            TargetLeyLane.SetRuneRotationSpeedToInitial();
            TargetLeyLane.ResetGlowToInitial();
            FlareImage.DOFade(0, 1).OnStepComplete(()=> FlareImage.gameObject.SetActive(false)); 
        }
    }

    private void HandleOnBackgroundScaled ()
    {
        DOTween.Sequence()
            .Join(TargetLeyLane.transform.DOScale(TargetLeyLaneTransform.localScale, 1))
            .Join(TargetLeyLane.transform.DORotateQuaternion(TargetLeyLaneTransform.rotation, 1))
            .Join(TargetLeyLane.transform.DOMove(TargetLeyLaneTransform.position, 1))
            .OnComplete(OnBackgroundSet);
        TargetLeyLane.SetRunesVisibility(1, 10);
    }

    private void SetCamera (Camera camera, bool setActive = true)
    {
        camera.transform.SetPositionAndRotation(MainCamera.transform.position, MainCamera.transform.rotation);
        camera.orthographicSize = MainCamera.orthographicSize;
        camera.gameObject.SetActive(setActive);
    }

    private void OnBackgroundSet ()
    {
        ForegroundCamera.gameObject.SetActive(true);
    }
}
