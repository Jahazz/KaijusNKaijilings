using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MultiCameraOverworldLayoutSystem : MonoBehaviour
{
    [field: Space]
    protected Actor FirstActor { get; set; }
    protected Actor SecondActor { get; set; }
    [field: SerializeField]
    protected Transform FirstActorTargetTransform { get; set; }
    [field: SerializeField]
    protected Transform SecondActorTargetTransform { get; set; }
    [field: SerializeField]
    public Camera CharactersCamera { get; private set; }
    [field: SerializeField]
    public Camera BackgroundCamera { get; private set; }
    [field: SerializeField]
    public Camera MainCamera { get; private set; }
    [field: SerializeField]
    public Camera GUICamera { get; private set; }
    protected float TargetNearClipPlane { get;  set; }
    protected float TargetFarClipPlane { get;  set; }
    protected float TargetFOV { get;  set; }
    protected string TargetActorLayerName { get; set; }
    protected abstract void HandleOnBackgroundEntered ();
    protected abstract void HandleOnCharactersMoved ();
    protected abstract void HandleOnZoomInCompleted ();
    protected abstract void HandleOnCharactersMovedBack ();
    protected abstract void HandleOnZoomOutCompleted ();

    protected void Initialize ()
    {
        SingletonContainer.Instance.OverworldPlayerCharacterManager.FreezePlayer();
        SetCamera(CharactersCamera);
        SetCamera(BackgroundCamera);
        SetCamera(MainCamera);
        SetCamera(GUICamera);
    }

    private void SetCamera (Camera camera)
    {
        camera.transform.SetPositionAndRotation(MainCamera.transform.position, MainCamera.transform.rotation);
        camera.orthographicSize = MainCamera.orthographicSize;
        camera.gameObject.SetActive(true);
    }

    protected Tween SetCameraValues ( float duration)
    {
        SetCameraFarNearPlanes(CharactersCamera);
        SetCameraFarNearPlanes(BackgroundCamera);
        SetCameraFarNearPlanes(GUICamera);

        return DOTween.Sequence()
            .Join(CharactersCamera.DOOrthoSize(TargetFOV, duration))
            .Join(BackgroundCamera.DOOrthoSize(TargetFOV, duration))
            .Join(GUICamera.DOOrthoSize(TargetFOV, duration));
    }

    private void SetCameraFarNearPlanes (Camera camera)
    {
        camera.nearClipPlane = TargetNearClipPlane;
        camera.farClipPlane = TargetFarClipPlane;
    }

    protected void InitializeBackgroundEnter (Tween backgroundEnter)
    {
        backgroundEnter.OnComplete(HandleOnBackgroundEntered);
    }

    protected void InitializeMovingCharacters (Tween moveCharacters)
    {
        moveCharacters.OnComplete(HandleOnCharactersMoved);
    }

    protected void InitializeZoomIn (Tween zoomIn)
    {
        zoomIn.OnComplete(HandleOnZoomInCompleted);
    }

    protected void InitializeMovingCharactersBack (Tween moveCharactersback)
    {
        moveCharactersback.OnComplete(HandleOnCharactersMovedBack);
    }
    protected void InitializeZoomOut (Tween zoomOut)
    {
        zoomOut.OnComplete(HandleOnZoomOutCompleted);
    }

    protected void InitializeBackgroundExit (Tween backgroundExit)
    {
        backgroundExit.OnComplete(HandleOnBackgroundExited);
    }
    protected virtual void HandleOnBackgroundExited ()
    {
        SingletonContainer.Instance.OverworldPlayerCharacterManager.UnfreezePlayer();
    }
}