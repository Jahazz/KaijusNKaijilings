using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GenericRangedSpiderGraph<Type> : MonoBehaviour
{
    [field: SerializeField]
    private List<GraphTypeNode<Type>> MaxNodes { get; set; }
    [field: SerializeField]
    private List<GraphTypeNode<Type>> ValueNodes { get; set; }
    [field: SerializeField]
    private RectTransform RectTransform { get; set; }
    [field: SerializeField]
    private RectTransform Center { get; set; }
    [field: SerializeField]
    private bool IsRotationRightSide { get; set; }

    private float Radius { get; set; }
    private float AngleBetweenNodes { get; set; }
    private Dictionary<Type, GraphTypeNode<Type>> DictionarizedValues { get; set; }

    private const float FULL_RECT_ANGLE = 360;


    public void UpdateNode (Type nodeType, Vector2 range, Vector2 value)
    {
        float spread = range.y - range.x;
        //float distanceToInnerNode = 
        float distanceToOuterNode = value.y / range.y;
        float distanceToInnerNode = value.x / range.y;
        DictionarizedValues[nodeType].OuterTransform.DOAnchorPos(Center.anchoredPosition + new Vector2(0, distanceToOuterNode * Radius), 0.5f);
        DictionarizedValues[nodeType].OuterTransform.DOAnchorPos(RotateVectorByAngleFromCenter(AngleBetweenNodes * DictionarizedValues[nodeType].Index, Center.anchoredPosition + new Vector2(0, distanceToOuterNode * Radius)),0.5f);
        DictionarizedValues[nodeType].InnerTransform.DOAnchorPos(Center.anchoredPosition + new Vector2(0, distanceToInnerNode * Radius), 0.5f);
        DictionarizedValues[nodeType].InnerTransform.DOAnchorPos(RotateVectorByAngleFromCenter(AngleBetweenNodes * DictionarizedValues[nodeType].Index, Center.anchoredPosition + new Vector2(0, distanceToInnerNode * Radius)), 0.5f);
    }

    protected virtual void Start ()
    {
        int angleCount = MaxNodes.Count;
        AngleBetweenNodes = FULL_RECT_ANGLE / angleCount;
        Radius = RectTransform.sizeDelta.x / 2;
        IndexNodes();
        GeneratePolygonBackground();
        DictionarizedValues = ValueNodes.ToDictionary(x => x.SelectedType, x => x);

    }

    private void GeneratePolygonBackground ()
    {
        for (int i = 0; i < MaxNodes.Count; i++)
        {
            MaxNodes[i].OuterTransform.anchoredPosition = Center.anchoredPosition + new Vector2(0, Radius);
            MaxNodes[i].OuterTransform.anchoredPosition = RotateVectorByAngleFromCenter(AngleBetweenNodes * i, Center.anchoredPosition + new Vector2(0, Radius));
        }
    }

    private void IndexNodes ()
    {
        for (int i = 0; i < ValueNodes.Count; i++)
        {
            ValueNodes[i].Index = i;
        }
    }

    private Vector2 RotateVectorByAngleFromCenter (float angle, Vector2 vectorToRotate)
    {
        return Quaternion.AngleAxis(angle, IsRotationRightSide == true ? Vector3.back : Vector3.forward) * vectorToRotate;
    }
}
