using MVC.List;
using MVC.SingleSelectableList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MVC.SelectableList
{
    public class SelectableListModel<ElementType, ElementData, ListView> : ListModel<ElementType, ElementData, ListView>
        where ElementType : SelectableListElement<ElementData>
        where ListView : SelectableListView<ElementType, ElementData>
    {
        public delegate void ElementSelectedArguments (ElementData selectedElementData);
        public event ElementSelectedArguments OnElementSelection;

        public override void Initialize (ListView currentView)
        {
            base.Initialize(currentView);

            CurrentView.OnElementSelection += HandleViewElementSelection;
        }

        private void HandleViewElementSelection (ElementData selectedElementData)
        {
            OnElementSelection?.Invoke(selectedElementData);
        }
    }
}
