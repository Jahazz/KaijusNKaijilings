using MVC.List;
using MVC.SingleSelectableList;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;

public class EntityListModel : BaseEntityListModel<EntityListView>
{
    public void ReorderEntityListByPattern (List<Entity> pattern)
    {
        SingletonContainer.Instance.PlayerManager.CurrentPlayer.EntitiesInEquipment.CollectionChanged -= HandleOnEntitiesInEquipmentCollectionChanged;

        ObservableCollection<Entity> currentEntities = SingletonContainer.Instance.PlayerManager.CurrentPlayer.EntitiesInEquipment;
        currentEntities.Clear();

        foreach (Entity entity in pattern)
        {
            currentEntities.Add(entity);
        }

        SingletonContainer.Instance.PlayerManager.CurrentPlayer.EntitiesInEquipment.CollectionChanged += HandleOnEntitiesInEquipmentCollectionChanged;
    }

    protected virtual void OnEnable ()
    {
        AttachEvents();
        CurrentView.SetActiveEntityDetailedScreenController(true);
        PopulateEntities();
    }

    protected virtual void OnDisable ()
    {
        DetachEvents();
        CurrentView.SetActiveEntityDetailedScreenController(false);
        ClearEntities();
    }

    private void AttachEvents ()
    {
        SingletonContainer.Instance.PlayerManager.CurrentPlayer.EntitiesInEquipment.CollectionChanged += HandleOnEntitiesInEquipmentCollectionChanged;
    }

    private void DetachEvents ()
    {
        SingletonContainer.Instance.PlayerManager.CurrentPlayer.EntitiesInEquipment.CollectionChanged -= HandleOnEntitiesInEquipmentCollectionChanged;
    }

    private void HandleOnEntitiesInEquipmentCollectionChanged (object sender, NotifyCollectionChangedEventArgs e)
    {
        ClearEntities();
        PopulateEntities();
    }

    private void ClearEntities ()
    {
        CurrentView.ClearList();
    }

    private void PopulateEntities ()
    {
        foreach (Entity singleEntity in SingletonContainer.Instance.PlayerManager.CurrentPlayer.EntitiesInEquipment)
        {
            CurrentView.AddNewItem(singleEntity);
        }

        CurrentView.SelectFirstElement();
    }
}
