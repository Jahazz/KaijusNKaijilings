using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MVC.List
{
    public abstract class ListView<ElementType, ElementData> : BaseView 
        where ElementType : ListElement<ElementData>
    {
        [field: Space]
        [field: Header("ListElements")]
        [field: SerializeField]
        private ElementType ElementToSpawn { get; set; }
        [field: SerializeField]
        private Transform ElementsContainer { get; set; }

        [field: Space]
        [field: SerializeField]
        private bool IsDraggable { get; set; }
        [field: SerializeField]
        private UnityListOrderChangedParams<ElementData> OnElementDropped { get; set; }

        public Dictionary<ElementData, ElementType> ContainingElementsCollection { get; private set; } = new Dictionary<ElementData, ElementType>();

        public virtual ElementType AddNewItem (ElementData elementData)
        {
            ElementType createdElement = Instantiate(ElementToSpawn, ElementsContainer);
            createdElement.Initialize(elementData);

            if (IsDraggable == true)
            {
                createdElement.SetupDragging();
                createdElement.OnElementDropped += HandleOnElementDropped;
            }

            ContainingElementsCollection.Add(elementData, createdElement);
            return createdElement;
        }

        public virtual void DestroyElement (ElementData elementData)
        {
            Destroy(ContainingElementsCollection[elementData]);
            ContainingElementsCollection[elementData].OnElementDropped -= HandleOnElementDropped;
            ContainingElementsCollection.Remove(elementData);
        }

        public void ClearList ()
        {
            Dictionary<ElementData, ElementType> ElementsToRemoveCollection = ContainingElementsCollection;

            foreach (KeyValuePair< ElementData, ElementType> element in ElementsToRemoveCollection)
            {
                Destroy(element.Value.gameObject);
            }

            ContainingElementsCollection.Clear();
        }

        private void HandleOnElementDropped ()
        {
            List<ElementData> reorganizedData = new List<ElementData>();

            for (int i = 0; i < ElementsContainer.childCount; i++)
            {
                foreach (KeyValuePair<ElementData, ElementType> element in ContainingElementsCollection)
                {
                    if (ElementsContainer.GetChild(i) == element.Value.transform)
                    {
                        reorganizedData.Add(element.Key);
                        break;
                    }
                }
            }

            OnElementDropped.Invoke(reorganizedData);
        }
    }
}
