using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MultiCameraOverworldLayoutSystem
{

    [field: SerializeField]
    private RectTransform Background { get; set; }
    [field: SerializeField]
    private RectTransform Foreground { get; set; }
    private float Duration = 1;

    public void Initialize (Player firstAnimator, Player secondAnimator)
    {
        TargetNearClipPlane = 0.0f;
        TargetFarClipPlane = 30.0f;
        TargetOrthoSize = 0.8f;

        Initialize();

        TargetActorLayerName = "Dialogue";
        FirstActor = new Actor(firstAnimator, TargetActorLayerName);
        SecondActor = new Actor(secondAnimator, TargetActorLayerName);

        InitializeBackgroundEnter(Background.DOScale(Vector3.one, Duration));
    }

    private Sequence MoveActor (Transform actorTransform, Vector3 position, Quaternion rotation)
    {
        return DOTween.Sequence()
            .Join(actorTransform.DOMove(position, Duration))
            .Join(actorTransform.DORotate(rotation.eulerAngles, Duration));
    }

    protected override void HandleOnBackgroundEntered ()
    {
        InitializeMovingCharacters(DOTween.Sequence()
                        .Join(MoveActor(FirstActor.Model.transform, FirstActorTargetTransform.position, FirstActorTargetTransform.rotation))
                        .Join(MoveActor(SecondActor.Model.transform, SecondActorTargetTransform.position, SecondActorTargetTransform.rotation)));
    }

    protected override void HandleOnCharactersMoved ()
    {
        CharactersCamera.nearClipPlane = TargetNearClipPlane;
        CharactersCamera.farClipPlane = TargetFarClipPlane;
        InitializeZoomIn(CharactersCamera.DOOrthoSize(TargetOrthoSize, Duration));
    }

    protected override void HandleOnZoomInCompleted ()
    {
        Close();
    }

    public void Close ()
    {
        CharactersCamera.nearClipPlane = MainCamera.nearClipPlane;
        CharactersCamera.farClipPlane = MainCamera.farClipPlane;
        InitializeZoomOut(CharactersCamera.DOOrthoSize(MainCamera.orthographicSize, Duration));
    }

    protected override void HandleOnZoomOutCompleted ()
    {
        InitializeMovingCharactersBack(DOTween.Sequence()
               .Join(MoveActor(FirstActor.Model.transform, FirstActor.InitialPosition, FirstActor.InitialRotation))
               .Join(MoveActor(SecondActor.Model.transform, SecondActor.InitialPosition, SecondActor.InitialRotation)));

    }

    protected override void HandleOnCharactersMovedBack ()
    {
        InitializeBackgroundExit(Background.DOScale(Vector3.zero, Duration / 2));
    }

    protected override void HandleOnBackgroundExited ()
    {
        base.HandleOnBackgroundExited();

        FirstActor.ResetLayer();
        SecondActor.ResetLayer();
    }
}
