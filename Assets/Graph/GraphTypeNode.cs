using System;
using UnityEngine;

[Serializable]
public class GraphTypeNode<Type>
{
    [field: SerializeField]
    public Type SelectedType { get; private set; }
    [field: SerializeField]
    public RectTransform InnerTransform { get; private set; }
    [field: SerializeField]
    public RectTransform OuterTransform { get; private set; }
    public float Index { get; set; }
}
