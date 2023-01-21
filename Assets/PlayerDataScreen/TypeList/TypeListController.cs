using MVC.List;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class TypeListController : ListController<TypeListElement, TypeDataScriptable, TypeListView, TypeListModel>
{
    public void Initialize (ObservableCollection<TypeDataScriptable> typeScriptableCollection)
    {
        CurrentModel.Initialize(typeScriptableCollection);
    }
}
