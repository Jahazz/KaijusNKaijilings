using MVC.SingleSelectableList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEntityListModel : SingleSelectableListModel<AllEntityListElement, StatsScriptable, AllEntityListView>
{

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
        //refresh entityListWhen discovered
    }

    private void DetachEvents ()
    {

    }

    //private void HandleOnEntitiesInEquipmentCollectionChanged (object sender, NotifyCollectionChangedEventArgs e)
    //{
    //    ClearEntities();
    //    PopulateEntities();
    //}

    private void ClearEntities ()
    {
        CurrentView.ClearList();
    }

    private void PopulateEntities ()
    {
        foreach (StatsScriptable singleEntity in SingletonContainer.Instance.EntityManager.AllEntitiesTypes)
        {
            CurrentView.AddNewItem(singleEntity);
        }

        CurrentView.SelectFirstElement();
    }
}
