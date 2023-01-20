using MVC.List;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MVC.SelectableList
{
    public class SelectableListView<ElementType, ElementData> : ListView<ElementType, ElementData>
        where ElementType : SelectableListElement<ElementData>
    {
        internal delegate void ElementSelectedArguments (ElementData selectedElementData);
        internal event ElementSelectedArguments OnElementSelection;

        public override ElementType AddNewItem (ElementData elementData)
        {
            ElementType createdElement = base.AddNewItem(elementData);
            createdElement.InitializeOnSelectionAction(SelectElement);
            return createdElement;
        }

        public void SelectFirstElement ()
        {
            if (ContainingElementsCollection.Count > 0)
            {
                ContainingElementsCollection.FirstOrDefault().Value.Select();
            }
        }

        private void SelectElement (ElementData elementData)
        {
            foreach (KeyValuePair<ElementData, ElementType> element in ContainingElementsCollection)
            {
                if (EqualityComparer<ElementData>.Default.Equals(element.Key, elementData) == false)
                {
                    element.Value.Deselect();
                }
            }

            OnElementSelection?.Invoke(elementData);
        }
    }
}
