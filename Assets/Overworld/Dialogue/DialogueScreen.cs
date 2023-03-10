using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueScreen : MonoBehaviour
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
    private Transform CameraPosition { get; set; }
    [field: SerializeField]
    private Camera DialogueCamera { get; set; }
    [field: SerializeField]
    private Camera BackgroundCamera { get; set; }
    [field: SerializeField]
    private Camera MainCamera { get; set; }
    [field: SerializeField]
    private RectTransform Background { get; set; }
    [field: SerializeField]
    private RectTransform Foreground { get; set; }

    private Actor FirstActor { get; set; }
    private Actor SecondActor { get; set; }
    private float Duration = 1;
    private float DefaultNearClipPlane { get; set; }
    private float DefaultFarClipPlane { get; set; }
    private float DefaultOrthoSize { get; set; }
    private float TargetNearClipPlane { get; set; } = 0.0f;
    private float TargetFarClipPlane { get; set; } = 30.0f;
    private float TargetOrthoSize { get; set; } = 0.8f;
    public void Initialize ()
    {
        FirstActor = new Actor(FirstAnimator);
        SecondActor = new Actor(SecondAnimator);
        DialogueCamera.transform.SetPositionAndRotation(MainCamera.transform.position, MainCamera.transform.rotation);
        SetupCamertaToDialogue();
    }

    private void SetupCamertaToDialogue ()
    {
        DialogueCamera.orthographicSize = MainCamera.orthographicSize;
        DialogueCamera.gameObject.SetActive(true);
        Background.DOScale(Vector3.one, Duration/2).OnComplete(HandleOnBackgroundEnter);
    }

    private void HandleOnBackgroundEnter ()
    {
        DefaultNearClipPlane = DialogueCamera.nearClipPlane;
        DefaultFarClipPlane = DialogueCamera.farClipPlane;
        DefaultOrthoSize = DialogueCamera.orthographicSize;

        DialogueCamera.DOOrthoSize(TargetOrthoSize, Duration);
        DialogueCamera.nearClipPlane = TargetNearClipPlane;
        DialogueCamera.farClipPlane = TargetFarClipPlane;
        DOTween.Sequence()
            .Join(MoveActor(FirstActor.Model.transform, FirstActorTargetTransform.position, FirstActorTargetTransform.rotation))
            .Join(MoveActor(SecondActor.Model.transform, SecondActorTargetTransform.position, SecondActorTargetTransform.rotation))
            .OnComplete(HandleOnCharactersZoom);
        
    }

    private void HandleOnCharactersZoom ()
    {
        CloseDialogue();
    }

    private Sequence MoveActor (Transform actorTransform, Vector3 position, Quaternion rotation)
    {
        return DOTween.Sequence()
            .Join(actorTransform.DOMove(position, Duration))
            .Join(actorTransform.DORotate(rotation.eulerAngles, Duration));
    }

    private void CloseDialogue ()
    {
        DOTween.Sequence()
            .Join(MoveActor(FirstActor.Model.transform, FirstActor.InitialPosition, FirstActor.InitialRotation))
            .Join(MoveActor(SecondActor.Model.transform, SecondActor.InitialPosition, SecondActor.InitialRotation))
            .Join(DialogueCamera.DOOrthoSize(DefaultOrthoSize, Duration))
            .OnComplete(HandleOnCharactersBack);
    }

    private void HandleOnCharactersBack ()
    {
        Background.DOScale(Vector3.zero, Duration / 2).OnComplete(HandleOnBackgroundExit);
        DialogueCamera.nearClipPlane = DefaultNearClipPlane;
        DialogueCamera.farClipPlane = DefaultFarClipPlane;
    }

    private void HandleOnBackgroundExit ()
    {
        FirstActor.ResetLayer();
        SecondActor.ResetLayer();
    }
}
