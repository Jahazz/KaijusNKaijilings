using MVC.List;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC.SelectableList
{
    public class SelectableListElement<ElementData> : ListElement<ElementData>
    {
        private Action<ElementData> OnSelectionAction { get; set; }
        private bool IsSelected { get; set; }
        protected ElementData CurrentElementData { get; set; }

        public virtual void InitializeOnSelectionAction (Action<ElementData> onSelectionAction)
        {
            OnSelectionAction = onSelectionAction;
        }

        public override void Initialize (ElementData elementData)
        {
            CurrentElementData = elementData;
        }

        public virtual void Deselect ()
        {
            IsSelected = false;
        }

        public virtual void Select ()
        {
            if (IsSelected == false)
            {
                IsSelected = true;
                OnSelectionAction?.Invoke(CurrentElementData);
            }
        }
    }
}
