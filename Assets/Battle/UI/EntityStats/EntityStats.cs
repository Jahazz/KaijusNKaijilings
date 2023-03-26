using Bindings;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Utils;
using UnityEngine;

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
    private RectTransform RectTransform { get; set; }
    [field: SerializeField]
    private GameObject ExperienceBar { get; set; }

    private Entity EntityToAttach { get; set; }
    private Transform TransfromToAttach { get; set; }
    private List<Binding> BindingsCollection { get; set; } = new List<Binding>();

    public void Initialize (Entity entityToAttach, Transform transfromToAttach, bool isPlayerOwner)
    {
        EntityToAttach = entityToAttach;
        TransfromToAttach = transfromToAttach;

        EntityCustomNameLabel.text = EntityToAttach.Name.PresentValue;
        EntityBaseNameLabel.text = EntityToAttach.BaseEntityType.Name;

        ExperienceBar.SetActive(isPlayerOwner);

        GenerateBindings();
    }

    protected virtual void Update ()
    {
        UpdatePosition();
    }

    protected virtual void OnDestroy ()
    {
        DisposeOfBindings();
    }

    private void GenerateBindings ()
    {
        BindingsCollection.Add(BindingFactory.GenerateCustomProgressBarBinding(HealthProgressBar, new ObservableVariable<float>(0), EntityToAttach.ModifiedStats.Health.MaxValue, EntityToAttach.ModifiedStats.Health.CurrentValue));
        BindingsCollection.Add(BindingFactory.GenerateCustomProgressBarBinding(ManaProgressBar, new ObservableVariable<float>(0), EntityToAttach.ModifiedStats.Mana.MaxValue, EntityToAttach.ModifiedStats.Mana.CurrentValue));
        BindingsCollection.Add(BindingFactory.GenerateCustomProgressBarBinding(ExperienceProgressBar, EntityToAttach.LevelData.ExperienceNeededForCurrentLevel, EntityToAttach.LevelData.ExperienceNeededForNextLevel, EntityToAttach.LevelData.CurrentExperience));
    }

    private void DisposeOfBindings ()
    {
        foreach (Binding binding in BindingsCollection)
        {
            binding.Unbind();
        }
    }

    private void UpdatePosition ()
    {
        Vector2 viewportPoint = SingletonContainer.Instance.BattleScreenManager.GUICamera.WorldToViewportPoint(TransfromToAttach.position);
        RectTransform.anchorMin = viewportPoint;
        RectTransform.anchorMax = viewportPoint;
        //TODO: ugghhsdasdaghvsba
    }
}
