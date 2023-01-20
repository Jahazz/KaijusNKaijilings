using System.Collections.Generic;
using UnityEngine;

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

        public Dictionary<ElementData, ElementType> ContainingElementsCollection { get; private set; } = new Dictionary<ElementData, ElementType>();

        public virtual ElementType AddNewItem (ElementData elementData)
        {
            ElementType createdElement = Instantiate(ElementToSpawn, ElementsContainer);
            createdElement.Initialize(elementData);
            ContainingElementsCollection.Add(elementData, createdElement);
            return createdElement;
        }

        public virtual void DestroyElement (ElementData elementData)
        {
            Destroy(ContainingElementsCollection[elementData]);
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
    }
}
