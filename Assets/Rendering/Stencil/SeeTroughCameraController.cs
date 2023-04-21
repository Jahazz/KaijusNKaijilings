using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SeeTroughCameraController : MonoBehaviour
{
    [field: SerializeField]
    private string SeeThroughLayerName { get; set; }
    [field: SerializeField]
    private Camera MainCamera { get; set; }
    [field: SerializeField]
    private float TargetMaskScale { get; set; }
    [field: SerializeField]
    private float TimeToResize { get; set; }

    private int SeeTroughLayerID { get; set; }

    protected virtual void Start ()
    {
        SeeTroughLayerID = LayerMask.NameToLayer(SeeThroughLayerName);
    }

    protected virtual void Update ()
    {
        ScaleMask(IsTaggedObjectBetweenCameraAndPlayer());
    }

    private bool IsTaggedObjectBetweenCameraAndPlayer ()
    {
        Vector3 startPos = MainCamera.transform.position;
        Vector3 endPos = transform.position;
        Vector3 direction = endPos - startPos;
        startPos -= direction * 5;
        direction = direction.normalized;
        float distance = Vector3.Distance(startPos, endPos);

        return Physics.Raycast(startPos, direction, out _, distance, ~SeeTroughLayerID);
    }

    private void ScaleMask (bool scaleToMax)
    {
        float targetMaskScale = scaleToMax == true ? TargetMaskScale : 0;
        transform.DOScale(targetMaskScale, TimeToResize);
    }
}

