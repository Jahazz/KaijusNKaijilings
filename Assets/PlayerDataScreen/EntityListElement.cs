using Bindings;
using MVC.SelectableList;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class EntityListElement : SelectableListElement<Entity>
{
    [field: SerializeField]
    private TMP_Text CustomNameLabel { get; set; }
    [field: SerializeField]
    private TMP_Text EntityNameLabel { get; set; }
    [field: SerializeField]
    private TMP_Text LevelLabel { get; set; }
    [field: SerializeField]
    private Image EntityImage { get; set; }
    [field: SerializeField]
    private string LevelLabelFormat { get; set; }
    [field: SerializeField]
    private GameObject SelectionBorder { get; set; }

    [field: Space]
    [field: SerializeField]
    private TypeListController TypeController { get; set; }

    [field: Space]
    [field: SerializeField]
    private CustomProgressBar HealthBar { get; set; }
    [field: SerializeField]
    private CustomProgressBar ManaBar { get; set; }
    [field: SerializeField]
    private CustomProgressBar ExperienceBar { get; set; }

    public override void Initialize (Entity elementData)
    {
        base.Initialize(elementData);

        InitializeBaseData();
        InitializeTypes();
        InitializeBars();

        CurrentElementData.LevelData.CurrentLevel.OnVariableChange += HandleOnLevelChange;
        CurrentElementData.Name.OnVariableChange += HandleOnNameChange;
    }

    private void HandleOnNameChange (string newValue)
    {
        CustomNameLabel.text = newValue;
    }

    private void HandleOnLevelChange (int newValue)
    {
        LevelLabel.text = string.Format(LevelLabelFormat, newValue);
    }

    private void InitializeBaseData ()
    {
        CustomNameLabel.text = CurrentElementData.Name.PresentValue;
        EntityNameLabel.text = CurrentElementData.BaseEntityType.Name;
        LevelLabel.text = string.Format(LevelLabelFormat, CurrentElementData.LevelData.CurrentLevel.PresentValue);
        EntityImage.sprite = CurrentElementData.BaseEntityType.Image;
    }

    private void InitializeTypes ()
    {
        TypeController.Initialize(CurrentElementData.TypeScriptableCollection);
    }

    private void InitializeBars ()
    {
        BindingFactory.GenerateCustomProgressBarBinding(HealthBar, new ObservableVariable<float>(0), CurrentElementData.ModifiedStats.Health.MaxValue, CurrentElementData.ModifiedStats.Health.CurrentValue);
        BindingFactory.GenerateCustomProgressBarBinding(ManaBar, new ObservableVariable<float>(0), CurrentElementData.ModifiedStats.Mana.MaxValue, CurrentElementData.ModifiedStats.Mana.CurrentValue);
        BindingFactory.GenerateCustomProgressBarBinding(ExperienceBar, CurrentElementData.LevelData.ExperienceNeededForCurrentLevel, CurrentElementData.LevelData.ExperienceNeededForNextLevel, CurrentElementData.LevelData.CurrentExperience);
    }

    public override void Select ()
    {
        SelectionBorder.SetActive(true);
        base.Select();
    }

    public override void Deselect ()
    {
        SelectionBorder.SetActive(false);
        base.Deselect();
    }
}
