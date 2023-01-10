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

        public List<ElementType> ContainingElements { get; private set; } = new List<ElementType>();

        public ElementType AddNewItem (ElementData elementData)
        {
            ElementType createdElement = Instantiate(ElementToSpawn, ElementsContainer);
            createdElement.Initialize(elementData);
            ContainingElements.Add(createdElement);
            return createdElement;
        }
    }
}
