using MVC.List;
using MVC.SingleSelectableList;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class EntityListModel : SingleSelectableListModel<EntityListElement, Entity, EntityListView>
{
    // Start is called before the first frame update
    protected virtual void OnEnable ()
    {
        AttachEvents();
        PopulateEntities();
    }

    protected virtual void OnDisable ()
    {
        DetachEvents();
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
