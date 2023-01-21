using MVC.List;
using MVC.SelectableList;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MVC.SingleSelectableList
{
    public class SingleSelectableListView<ElementType, ElementData> : SelectableListView<ElementType, ElementData>
        where ElementType : SingleSelectableListElement<ElementData>
    {
        protected override void HandleOnElementSelection (ElementData elementData, bool isSelected)
        {
            foreach (KeyValuePair<ElementData, ElementType> element in ContainingElementsCollection)
            {
                if (EqualityComparer<ElementData>.Default.Equals(element.Key, elementData) == false)
                {
                    element.Value.Deselect();
                }
            }

            base.HandleOnElementSelection(elementData, isSelected);
        }
    }
}
