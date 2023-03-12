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
    protected Camera CharactersCamera { get; private set; }
    [field: SerializeField]
    protected Camera BackgroundCamera { get; private set; }
    [field: SerializeField]
    protected Camera MainCamera { get; private set; }
    [field: SerializeField]
    protected Camera GUICamera { get; private set; }
    protected float TargetNearClipPlane { get;  set; }
    protected float TargetFarClipPlane { get;  set; }
    protected float TargetOrthoSize { get;  set; }
    protected string TargetActorLayerName { get; set; }
    protected abstract void HandleOnBackgroundEntered ();
    protected abstract void HandleOnCharactersMoved ();
    protected abstract void HandleOnZoomInCompleted ();
    protected abstract void HandleOnCharactersMovedBack ();
    protected abstract void HandleOnZoomOutCompleted ();

    protected void Initialize ()
    {
        SingletonContainer.Instance.OverworldPlayerCharacterManager.FreezePlayer();
        CharactersCamera.transform.SetPositionAndRotation(MainCamera.transform.position, MainCamera.transform.rotation);
        CharactersCamera.orthographicSize = MainCamera.orthographicSize;
        CharactersCamera.gameObject.SetActive(true);
        BackgroundCamera.gameObject.SetActive(true);
        GUICamera.gameObject.SetActive(true);
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