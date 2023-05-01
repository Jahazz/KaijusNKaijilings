using Bindings;
using MVC.SingleSelectableList;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class EntityListElement : BaseEntityListElement
{
    [field: SerializeField]
    private TMP_Text CustomNameLabel { get; set; }
    [field: SerializeField]
    private TMP_Text LevelLabel { get; set; }
    [field: SerializeField]
    private string LevelLabelFormat { get; set; }

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

    protected override void InitializeBaseData ()
    {
        base.InitializeBaseData();

        CustomNameLabel.text = CurrentElementData.Name.PresentValue;
        LevelLabel.text = string.Format(LevelLabelFormat, CurrentElementData.LevelData.CurrentLevel.PresentValue);
    }

    private void InitializeBars ()
    {
        BindingFactory.GenerateCustomProgressBarBinding(HealthBar, new ObservableVariable<float>(0), CurrentElementData.ModifiedStats.Health.MaxValue, CurrentElementData.ModifiedStats.Health.CurrentValue);
        BindingFactory.GenerateCustomProgressBarBinding(ManaBar, new ObservableVariable<float>(0), CurrentElementData.ModifiedStats.Mana.MaxValue, CurrentElementData.ModifiedStats.Mana.CurrentValue);
        BindingFactory.GenerateCustomProgressBarBinding(ExperienceBar, CurrentElementData.LevelData.ExperienceNeededForCurrentLevel, CurrentElementData.LevelData.ExperienceNeededForNextLevel, CurrentElementData.LevelData.CurrentExperience);
    }
}
