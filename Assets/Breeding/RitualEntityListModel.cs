using MVC.SelectableList;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class RitualEntityListModel : SelectableListModel<RitualEntityListElement, Entity, RitualEntityListView>
{
    private List<Entity> RitualParentsColection { get; set; } = new List<Entity>();

    private const string REQUIRED_ENTITIES_BUTTON_LABEL_FORMAT = "{0} of {1}";
    private const string BREED_LABEL_TEXT = "Start ritual";

    public void StartRitual ()
    {
        CurrentView.ShowSummonedEntity(SingletonContainer.Instance.BreedingManager.Breed(SingletonContainer.Instance.PlayerManager.CurrentPlayer, RitualParentsColection));

        RitualParentsColection.Clear();

        foreach (var item in CurrentView.ContainingElementsCollection)
        {
            HandleOnElementSelection(item.Key, false);
        }
    }

    protected override void Awake ()
    {
        base.Awake();
        PopulateParentsTable();
        SetupTraitCollection();

    }

    protected override void AttachToEvents ()
    {
        base.AttachToEvents();

        SingletonContainer.Instance.PlayerManager.CurrentPlayer.EntitiesInEquipment.CollectionChanged += HandleEntitiesInEquipmentCHanged;
        OnElementSelection += HandleOnElementSelection;
    }

    protected override void DetachFromEvents ()
    {
        base.DetachFromEvents();

        SingletonContainer.Instance.PlayerManager.CurrentPlayer.EntitiesInEquipment.CollectionChanged -= HandleEntitiesInEquipmentCHanged;
        OnElementSelection -= HandleOnElementSelection;
    }

    private void PopulateParentsTable ()
    {
        CurrentView.ClearList();

        foreach (Entity entity in SingletonContainer.Instance.PlayerManager.CurrentPlayer.EntitiesInEquipment)
        {
            CurrentView.AddNewItem(entity);
        }
    }

    private void SetupTraitCollection ()
    {
        CurrentView.GenerateExpectedTraitList(AvaiableTraits);
    }

    private void HandleEntitiesInEquipmentCHanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        PopulateParentsTable();
    }


    private void HandleOnElementSelection (Entity selectedElementData, bool isSelected)
    {
        if (isSelected == true)
        {
            RitualParentsColection.Add(selectedElementData);
        }
        else
        {
            RitualParentsColection.Remove(selectedElementData);
        }

        CurrentView.UpdateResultStats(SingletonContainer.Instance.BreedingManager.GetStatRangesForSelectedParents(RitualParentsColection), selectedElementData.BaseEntityType.BaseMatRange);
        UpdateAvailableTraits();

        int requiredEntities = selectedElementData.BaseEntityType.GroupCountRequiredToBreed;
        bool isEntityCountMax = requiredEntities == RitualParentsColection.Count;

        foreach (KeyValuePair<Entity, RitualEntityListElement> entity in CurrentView.ContainingElementsCollection)
        {
            
            bool isEntityOtherType =(entity.Key.BaseEntityType != selectedElementData.BaseEntityType && RitualParentsColection.Count > 0);

            if (entity.Value.IsSelected == false)
            {
                entity.Value.SetAvaliability(RitualParentsColection.Count == 0 || (isEntityOtherType == false && isEntityCountMax == false));
            }
        }

        CurrentView.SetAcceptButtonInteractable(isEntityCountMax == true);
        CurrentView.SetAcceptButtonText(isEntityCountMax == true? BREED_LABEL_TEXT : string.Format(REQUIRED_ENTITIES_BUTTON_LABEL_FORMAT, RitualParentsColection.Count, requiredEntities));
    }

    ObservableCollection<TraitBaseScriptableObject> AvaiableTraits = new ObservableCollection<TraitBaseScriptableObject>();

    private void UpdateAvailableTraits ()
    {
        AvaiableTraits.Clear();

        foreach (Entity entity in RitualParentsColection)
        {
            foreach (TraitBaseScriptableObject trait in entity.TraitsCollection)
            {
                if (AvaiableTraits.Contains(trait) == false)
                {
                    AvaiableTraits.Add(trait);
                }
            }
        }
    }
}
