using MVC.List;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class TypeListModel : ListModel<TypeListElement, TypeDataScriptable, TypeListView>
{
    ObservableCollection<TypeDataScriptable> TypeScriptableCollection { get; set; }

    public void Initialize (ObservableCollection<TypeDataScriptable> typeScriptableCollection)
    {
        if (TypeScriptableCollection!= null)
        {
            TypeScriptableCollection.CollectionChanged -= HandleOnTypeScriptableCollectionChanged;
        }

        TypeScriptableCollection = typeScriptableCollection;

        ReinitializeCollection();
        TypeScriptableCollection.CollectionChanged += HandleOnTypeScriptableCollectionChanged;
    }

    private void HandleOnTypeScriptableCollectionChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        ReinitializeCollection();
    }

    public void ReinitializeCollection ()
    {
        CurrentView.ClearList();

        foreach (TypeDataScriptable item in TypeScriptableCollection)
        {
            CurrentView.AddNewItem(item);
        }
    }
}
