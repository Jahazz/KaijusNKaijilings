using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueScreen : MultiCameraOverworldLayoutSystem
{
    [field: SerializeField]
    private Animator FirstAnimator { get; set; }
    [field: SerializeField]
    private Animator SecondAnimator { get; set; }

    [field: Space]
    [field: SerializeField]
    private Transform FirstActorTargetTransform { get; set; }
    [field: SerializeField]
    private Transform SecondActorTargetTransform { get; set; }

    [field: SerializeField]
    private RectTransform Background { get; set; }
    [field: SerializeField]
    private RectTransform Foreground { get; set; }
    private Actor FirstActor { get; set; }
    private Actor SecondActor { get; set; }
    private float Duration = 1;

    public override void Initialize ()
    {
        base.Initialize();
        TargetActorLayerName = "Dialogue";
        FirstActor = new Actor(FirstAnimator, TargetActorLayerName);
        SecondActor = new Actor(SecondAnimator, TargetActorLayerName);

        InitializeBackgroundEnter(Background.DOScale(Vector3.one, Duration));
    }

    private Sequence MoveActor (Transform actorTransform, Vector3 position, Quaternion rotation)
    {
        return DOTween.Sequence()
            .Join(actorTransform.DOMove(position, Duration))
            .Join(actorTransform.DORotate(rotation.eulerAngles, Duration));
    }

    //private void CloseDialogue ()
    //{
    //    DOTween.Sequence()
    //        .Join(MoveActor(FirstActor.Model.transform, FirstActor.InitialPosition, FirstActor.InitialRotation))
    //        .Join(MoveActor(SecondActor.Model.transform, SecondActor.InitialPosition, SecondActor.InitialRotation))
    //        .Join(CharactersCamera.DOOrthoSize(MainCamera.orthographicSize, Duration))
    //        .OnComplete(HandleOnCharactersBack);
    //}

    //private void HandleOnCharactersBack ()
    //{
    //    Background.DOScale(Vector3.zero, Duration / 2).OnComplete(HandleOnBackgroundExit);
    //    CharactersCamera.nearClipPlane = MainCamera.nearClipPlane;
    //    CharactersCamera.farClipPlane = MainCamera.farClipPlane;
    //}

    //protected void HandleOnBackgroundExit ()
    //{
    //    FirstActor.ResetLayer();
    //    SecondActor.ResetLayer();
    //}

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
        FirstActor.ResetLayer();
        SecondActor.ResetLayer();
    }
}
