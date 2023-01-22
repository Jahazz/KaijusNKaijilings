using MVC.List;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC.SelectableList
{
    public class SelectableListElement<ElementData> : ListElement<ElementData>
    {
        private Action<ElementData, bool> OnSelectionAction { get; set; }
        protected bool IsSelected { get; private set; }
        protected ElementData CurrentElementData { get; set; }

        public virtual void InitializeOnSelectionAction (Action<ElementData, bool> onSelectionAction)
        {
            OnSelectionAction = onSelectionAction;
        }

        public override void Initialize (ElementData elementData)
        {
            CurrentElementData = elementData;
        }

        public virtual void Deselect ()
        {
            if (IsSelected == true)
            {
                IsSelected = false;
                OnSelectionAction?.Invoke(CurrentElementData, IsSelected);
            }
        }

        public virtual void Select ()
        {
            if (IsSelected == false)
            {
                IsSelected = true;
                OnSelectionAction?.Invoke(CurrentElementData, IsSelected);
            }
        }
    }
}
