using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVC.SelectableList;
using TMPro;
using UnityEngine.UI;

public class RitualEntityListElement : SelectableListElement<Entity>
{
    [field: SerializeField]
    private TMP_Text CustomNameLabel { get; set; }
    [field: SerializeField]
    private TMP_Text NameLabel { get; set; }
    [field: SerializeField]
    private List<SliderStatPair> SliderStatPairCollection { get; set; }
    [field: SerializeField]
    private GameObject SelectionMarker { get; set; }
    [field: SerializeField]
    private GameObject UnavailableMarker { get; set; }
    [field: SerializeField]
    private Button SelectorButton { get; set; } 

    public override void Initialize (Entity elementData)
    {
        base.Initialize(elementData);

        CustomNameLabel.text = elementData.Name.PresentValue;
        NameLabel.text = elementData.BaseEntityType.Name;

        foreach (SliderStatPair item in SliderStatPairCollection)
        {
            Vector2 baseStatRange = elementData.BaseEntityType.BaseMatRange.GetStatOfType(item.StatType);
            item.ProgressBar.SetValue(baseStatRange.x, baseStatRange.y, elementData.MatStats.GetStatOfType(item.StatType));
        }
    }

    public void ToggleSelection ()
    {
        if(IsSelected == true)
        {
            Deselect();
        }
        else
        {
            Select();
        }
    }

    public override void Select ()
    {
        base.Select();
        SelectionMarker.SetActive(true);
    }

    public override void Deselect ()
    {
        base.Deselect();
        SelectionMarker.SetActive(false);
    }

    public void SetAvaliability (bool isAvailable)
    {
        UnavailableMarker.SetActive(isAvailable == false);
        SelectorButton.interactable = isAvailable;
    }
}
