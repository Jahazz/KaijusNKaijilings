using MVC.SelectableList;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillListElement : SelectableListElement<SkillScriptableObject>
{
    [field: SerializeField]
    private Image SkillImage { get; set; }
    [field: SerializeField]
    private TMP_Text SkillNameLabel { get; set; }
    [field: SerializeField]
    private TMP_Text SkillDescriptionLabel { get; set; }
    [field: SerializeField]
    private TMP_Text SkillCostLabel { get; set; }
    [field: SerializeField]
    private string SkillCostLabelFormat { get; set; }
    [field: SerializeField]
    private TypeListController TypeListController { get; set; }
    [field: SerializeField]
    private Toggle SelectionToggle { get; set; }

    public override void Initialize (SkillScriptableObject elementData)
    {
        base.Initialize(elementData);

        SkillImage.sprite = elementData.BaseSkillData.Image;
        SkillNameLabel.text = elementData.Name;
        SkillDescriptionLabel.text = elementData.Description;
        SkillCostLabel.text = string.Format(SkillCostLabelFormat, elementData.BaseSkillData.Cost);
        TypeListController.Initialize(new ObservableCollection<TypeDataScriptable>(elementData.BaseSkillData.SkilType));
    }

    public void SetSelection (bool value)
    {
        if (value == true)
        {
            Select();
        }
        else
        {
            Deselect();
        }
    }

    public void SetActiveOfToggle(bool value)
    {
        SelectionToggle.interactable = value;
    }

    public override void Select ()
    {
        base.Select();
        SetToggleIfNeeded(true);
    }

    public override void Deselect ()
    {
        base.Deselect();
        SetToggleIfNeeded(false);
    }

    private void SetToggleIfNeeded (bool value)
    {
        if (SelectionToggle.isOn != value)
        {
            SelectionToggle.isOn = value;
        }
    }

}
