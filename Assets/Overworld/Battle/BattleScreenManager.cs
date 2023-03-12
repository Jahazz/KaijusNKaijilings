using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleScreenManager : MultiCameraOverworldLayoutSystem
{

    [field: SerializeField]
    private RectTransform Background { get; set; }
    [field: SerializeField]
    private RectTransform Foreground { get; set; }
    [field: SerializeField]
    private Image BackgroundImage { get; set; }
    private float Duration = 1;

    public void Initialize (Animator firstAnimator, Animator secondAnimator)
    {
        TargetNearClipPlane = 0.0f;
        TargetFarClipPlane = 30.0f;
        TargetOrthoSize = 3f;

        Initialize();

        TargetActorLayerName = "Dialogue";
        FirstActor = new Actor(firstAnimator, TargetActorLayerName);
        SecondActor = new Actor(secondAnimator, TargetActorLayerName);


        Material bgm = BackgroundImage.material;
        bgm.SetFloat("_BlendFactor", 0);
        InitializeBackgroundEnter(DOTween.To(()=>bgm.GetFloat("_BlendFactor"),(float a)=>bgm.SetFloat("_BlendFactor", a),1,5));
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
