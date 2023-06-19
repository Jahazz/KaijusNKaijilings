using BattleCore;
using Bindings;
using MVC;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class EntityDetailedScreenView : BaseEntityDetailedScreenView
{
    [field: SerializeField]
    private TMP_InputField CustomNameInputField { get; set; }

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
    [field: SerializeField]
    private TraitListController TraitListController { get; set; }
    [field: SerializeField]
    private Button SummonEntityButton { get; set; }

    [field: Space]
    [field: SerializeField]
    private Button SpellbookButton { get; set; }
    [field: SerializeField]
    private Button SummonButton { get; set; }

    private List<Binding> BindingsCollection { get; set; } = new List<Binding>();
    private bool ShouldSummonButtonBeActive { get; set; }

    public override void SetData (Entity entity)
    {
        base.SetData(entity);

        UnbindBindings();
        SetupChangableData();
        TypeListColtroler.Initialize(entity.TypeScriptableCollection);
        TraitListController.InitializeTraits(entity.TraitsCollection);
        HideSummonButtonIfEntityIsInBattleOrDead();
    }

    public void SetupChangableData ()
    {
        foreach (StatTypeStatElementPair statType in StatElementCollection)
        {
            BindingsCollection.Add(BindingFactory.GenerateStatElementBinding(statType.StatElement, CurrentEntityData.StatsGainedThroughLeveling.GetStatOfType(statType.StatType), CurrentEntityData.ModifiedStats.GetStatOfType(statType.StatType), statType.StatType));
        }

        BindingsCollection.Add(BindingFactory.GenerateCustomProgressBarBinding(HealthBar, new ObservableVariable<float>(0), CurrentEntityData.ModifiedStats.Health.MaxValue, CurrentEntityData.ModifiedStats.Health.CurrentValue));
        BindingsCollection.Add(BindingFactory.GenerateCustomProgressBarBinding(ManaBar, new ObservableVariable<float>(0), CurrentEntityData.ModifiedStats.Mana.MaxValue, CurrentEntityData.ModifiedStats.Mana.CurrentValue));
        BindingsCollection.Add(BindingFactory.GenerateCustomProgressBarBinding(ExperienceBar, CurrentEntityData.LevelData.ExperienceNeededForCurrentLevel, CurrentEntityData.LevelData.ExperienceNeededForNextLevel, CurrentEntityData.LevelData.CurrentExperience));
        BindingsCollection.Add(BindingFactory.GenerateInputFieldBinding(CustomNameInputField, "{0}",true, CurrentEntityData.Name));
    }

    public void UnbindBindings ()
    {
        foreach (Binding binding in BindingsCollection)
        {
            binding.Unbind();
        }

        BindingsCollection.Clear();
    }

    public void SetSummonButtonVisibility (bool isVisible)
    {
        SummonEntityButton.gameObject.SetActive(isVisible == true);
    }

    public void SetButtonsVisibility (bool isSpellbookButtonShow, bool isSummonButtonShown)
    {
        SpellbookButton.gameObject.SetActive(isSpellbookButtonShow);
        ShouldSummonButtonBeActive = isSummonButtonShown;
    }

    private void HideSummonButtonIfEntityIsInBattleOrDead ()
    {
        if (BattleFactory.CurrentBattle != null && ShouldSummonButtonBeActive == true && CurrentEntityData.IsAlive.PresentValue == true)
        {
            SummonButton.gameObject.SetActive(CurrentEntityData != BattleFactory.CurrentBattle.GetPlayerBattleParticipant().CurrentEntity.PresentValue);
        }
        else
        {
            SummonButton.gameObject.SetActive(false);
        }
    }
}
