using MVC.List;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeListElement : ListElement<TypeDataScriptable>
{
    [field: SerializeField]
    private TypeIcon TypeIcon { get; set; }

    public override void Initialize (TypeDataScriptable elementData)
    {
        TypeIcon.SetIcon(elementData.TypeSprite);
    }
}
