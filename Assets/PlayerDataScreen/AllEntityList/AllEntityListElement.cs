using MVC.SingleSelectableList;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AllEntityListElement : SingleSelectableListElement<StatsScriptable>
{
    [field: SerializeField]
    private GameObject SelectionBorder { get; set; }
    [field: SerializeField]
    private Image EntityImage { get; set; }
    [field: SerializeField]
    private TMP_Text EntityNameLabel { get; set; }

    [field: Space]
    [field: SerializeField]
    private TypeListController TypeController { get; set; }

    public override void Initialize (StatsScriptable elementData)
    {
        base.Initialize(elementData);

        InitializeBaseData();
        InitializeTypes();
    }

    protected virtual void InitializeBaseData ()
    {
        EntityNameLabel.text = CurrentElementData.Name;
        EntityImage.sprite = CurrentElementData.Image;
    }

    public override void Select ()
    {
        base.Select();

        if (IsSelected == true)
        {
            SelectionBorder.SetActive(true);
        }
    }

    public override void Deselect ()
    {
        base.Deselect();

        if (IsSelected == false)
        {
            SelectionBorder.SetActive(false);
        }
    }

    private void InitializeTypes ()
    {
        TypeController.Initialize(new ObservableCollection<TypeDataScriptable>(CurrentElementData.EntityTypeCollection));
    }
}
