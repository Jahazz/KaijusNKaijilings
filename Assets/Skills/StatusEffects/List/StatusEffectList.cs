using StatusEffects;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class StatusEffectList<StatusType> : MonoBehaviour
{
    [field: SerializeField]
    private StatusEffectElement<StatusType> ElementToSpawn { get; set; }
    [field: SerializeField]
    private Transform ElementsContainer { get; set; }

    private List<StatusEffectElement<StatusType>> AllElementsCollection = new List<StatusEffectElement<StatusType>>();
    private ObservableCollection<StatusType> SourceCollection;
    public void Initialize (ObservableCollection<StatusType> sourceCollection)
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
        foreach (StatusEffectElement<StatusType> singleElement in AllElementsCollection)
        {
            Destroy(singleElement.gameObject);
        }

        AllElementsCollection.Clear();
    }

    private void InitializeFromSourceList ()
    {
        foreach (StatusType singleStatusEffect in SourceCollection)
        {
            CreateNewElement(singleStatusEffect);
        }
    }

    private void CreateNewElement (StatusType sourceData)
    {
        StatusEffectElement<StatusType> createdElement = Instantiate(ElementToSpawn, ElementsContainer);
        createdElement.Initialize(sourceData);
        AllElementsCollection.Add(createdElement);
    }
}
