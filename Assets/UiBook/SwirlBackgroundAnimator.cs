using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SwirlBackgroundAnimator : MonoBehaviour
{
    [field: SerializeField]
    private MeshRenderer MeshRendererInstance { get; set; }
    [field: SerializeField]
    private float MinValue { get; set; }
    [field: SerializeField]
    private float MaxValue { get; set; }
    [field: SerializeField]
    private float Time { get; set; }

    private float progressValue;
    private float ProgressValue {
        get {
            return progressValue;
        }
        set {
            progressValue = value;
            UpdateMaterialProgress();
        }
    }

    public void Show ()
    {
        DoTween(MaxValue);
    }
    public void Hide ()
    {
        DoTween(MinValue);
    }

    private void DoTween(float targetValue)
    {
        DOTween.To(() => ProgressValue, x => ProgressValue = x, targetValue, Time);
    }

    private void UpdateMaterialProgress ()
    {
        MeshRendererInstance.material.SetFloat("_Progress", ProgressValue);
    }
}
