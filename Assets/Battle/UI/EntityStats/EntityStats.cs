using Bindings;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Utils;
using UnityEngine;
using StatusEffects.EntityStatusEffects;

public class EntityStats : MonoBehaviour
{
    [field: SerializeField]
    private TMP_Text EntityCustomNameLabel { get; set; }
    [field: SerializeField]
    private TMP_Text EntityBaseNameLabel { get; set; }
    [field: SerializeField]
    private CustomProgressBar HealthProgressBar { get; set; }
    [field: SerializeField]
    private CustomProgressBar ManaProgressBar { get; set; }
    [field: SerializeField]
    private CustomProgressBar ExperienceProgressBar { get; set; }
    [field: SerializeField]
    private GameObject ExperienceBar { get; set; }
    [field: SerializeField]
    private StatusEffectList<BaseEntityScriptableStatusEffect> StatusEffectList { get; set; }

    private Entity EntityToAttach { get; set; }
    private List<Binding> BindingsCollection { get; set; } = new List<Binding>();

    public void Initialize (Entity entityToAttach, bool isPlayerOwner)
    {
        EntityToAttach = entityToAttach;

        EntityCustomNameLabel.text = EntityToAttach.Name.PresentValue;
        EntityBaseNameLabel.text = EntityToAttach.BaseEntityType.Name;
        StatusEffectList.Initialize(EntityToAttach.PresentStatusEffects);

        ExperienceBar.SetActive(isPlayerOwner);
        gameObject.SetActive(true);

        GenerateBindings();
    }

    private void GenerateBindings ()
    {
        BindingsCollection.Add(BindingFactory.GenerateCustomProgressBarBinding(HealthProgressBar, new ObservableVariable<float>(0), EntityToAttach.ModifiedStats.Health.MaxValue, EntityToAttach.ModifiedStats.Health.CurrentValue));
        BindingsCollection.Add(BindingFactory.GenerateCustomProgressBarBinding(ManaProgressBar, new ObservableVariable<float>(0), EntityToAttach.ModifiedStats.Mana.MaxValue, EntityToAttach.ModifiedStats.Mana.CurrentValue));
        BindingsCollection.Add(BindingFactory.GenerateCustomProgressBarBinding(ExperienceProgressBar, EntityToAttach.LevelData.ExperienceNeededForCurrentLevel, EntityToAttach.LevelData.ExperienceNeededForNextLevel, EntityToAttach.LevelData.CurrentExperience));
    }

    public void DisposeOfBindings ()
    {
        foreach (Binding binding in BindingsCollection)
        {
            binding.Unbind();
        }
    }
}
