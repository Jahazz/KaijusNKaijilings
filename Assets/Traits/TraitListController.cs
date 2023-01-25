using MVC.List;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class TraitListController : ListController<TraitListElement, TraitBaseScriptableObject, TraitListView, TraitListModel>
{
    public void InitializeTraits (ObservableCollection<TraitBaseScriptableObject> traitDataCollection)
    {
        CurrentModel.InitializeTraits(traitDataCollection);
    }
}
