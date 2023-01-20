using Bindings;
using MVC;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class EntityDetailedScreenView : BaseView
{
    [field: SerializeField]
    private TMP_InputField CustomNameInputField { get; set; }
    [field: SerializeField]
    private TMP_Text DefaultNameLabel { get; set; }
    [field: SerializeField]
    private TMP_Text DescriptionLabel { get; set; }
    [field: SerializeField]
    private Image EntityImage { get; set; }
    [field: SerializeField]
    private List<StatTypeStatElementPair> StatElementCollection { get; set; }

    [field: Space]
    [field: SerializeField]
    private CustomProgressBar HealthBar { get; set; }
    [field: SerializeField]
    private CustomProgressBar ManaBar { get; set; }
    [field: SerializeField]
    private CustomProgressBar ExperienceBar { get; set; }
    [field: SerializeField]
    private TypeListController TypeListColtroler { get; set; }

    private List<Binding> BindingsCollection { get; set; } = new List<Binding>();
    private Entity CurrentEntityData { get; set; }

    public void SetData(Entity entity)
    {
        CurrentEntityData = entity;
        UnbindBindings();
        SetPersistentData();
        SetupChangableData();
        TypeListColtroler.Initialize(entity.TypeScriptableCollection);
    }

    public void SetPersistentData ()
    {
        DefaultNameLabel.text = CurrentEntityData.BaseEntityType.Name;
        DescriptionLabel.text = CurrentEntityData.BaseEntityType.Description;
        EntityImage.sprite = CurrentEntityData.BaseEntityType.Image;
    }

    public void SetupChangableData ()
    {
        foreach (StatTypeStatElementPair statType in StatElementCollection)
        {
            BindingsCollection.Add(BindingFactory.GenerateStatElementBinding(statType.StatElement, CurrentEntityData.StatsGainedThroughLeveling.GetStatOfType(statType.StatType), CurrentEntityData.ModifiedStats.GetStatOfType(statType.StatType)));
        }

        BindingsCollection.Add(BindingFactory.GenerateCustomProgressBarBinding(HealthBar, new ObservableVariable<float>(0), CurrentEntityData.ModifiedStats.Health.MaxValue, CurrentEntityData.ModifiedStats.Health.CurrentValue));
        BindingsCollection.Add(BindingFactory.GenerateCustomProgressBarBinding(ManaBar, new ObservableVariable<float>(0), CurrentEntityData.ModifiedStats.Mana.MaxValue, CurrentEntityData.ModifiedStats.Mana.CurrentValue));
        BindingsCollection.Add(BindingFactory.GenerateCustomProgressBarBinding(ExperienceBar, CurrentEntityData.LevelData.ExperienceNeededForCurrentLevel, CurrentEntityData.LevelData.ExperienceNeededForNextLevel, CurrentEntityData.LevelData.CurrentExperience));
        BindingsCollection.Add(BindingFactory.GenerateInputFieldBinding(CustomNameInputField,"{0}", CurrentEntityData.Name));


    }

    public void UnbindBindings ()
    {
        foreach (Binding binding in BindingsCollection)
        {
            binding.Unbind();
        }

        BindingsCollection.Clear();
    }


}
