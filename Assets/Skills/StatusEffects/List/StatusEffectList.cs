using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class StatusEffectList : MonoBehaviour
{
    [field: SerializeField]
    private StatusEffectElement ElementToSpawn { get; set; }
    [field: SerializeField]
    private Transform ElementsContainer { get; set; }

    private List<StatusEffectElement> AllElementsCollection = new List<StatusEffectElement>();
    private ObservableCollection<BaseStatusEffect> SourceCollection;
    public void Initialize (ObservableCollection<BaseStatusEffect> sourceCollection)
    {
        ClearList();
        SourceCollection = sourceCollection;
        SourceCollection.CollectionChanged += HandleSourceCollectionChanged;
    }

    protected virtual void OnDestroy ()
    {
        if (SourceCollection != null)
        {
            SourceCollection.CollectionChanged -= HandleSourceCollectionChanged;
        }
    }

    private void HandleSourceCollectionChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        ClearList();
        InitializeFromSourceList();
    }

    private void ClearList ()
    {
        foreach (StatusEffectElement singleElement in AllElementsCollection)
        {
            Destroy(singleElement.gameObject);
        }

        AllElementsCollection.Clear();
    }

    private void InitializeFromSourceList ()
    {
        foreach (BaseStatusEffect singleStatusEffect in SourceCollection)
        {
            CreateNewElement(singleStatusEffect);
        }
    }

    private void CreateNewElement (BaseStatusEffect sourceData)
    {
        StatusEffectElement createdElement = Instantiate(ElementToSpawn, ElementsContainer);
        createdElement.Initialize(sourceData);
        AllElementsCollection.Add(createdElement);
    }
}
