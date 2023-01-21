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
        public delegate void ElementSelectedArguments (ElementData selectedElementData);
        public event ElementSelectedArguments OnElementSelection;

        public override ElementType AddNewItem (ElementData elementData)
        {
            ElementType createdElement = base.AddNewItem(elementData);
            createdElement.InitializeOnSelectionAction(HandleOnElementSelection);
            return createdElement;
        }

        public void SelectFirstElement ()
        {
            if (ContainingElementsCollection.Count > 0)
            {
                ContainingElementsCollection.FirstOrDefault().Value.Select();
            }
        }

        protected virtual void HandleOnElementSelection (ElementData elementData)
        {
            OnElementSelection?.Invoke(elementData);
        }
    }
}

