using MVC.List;
using MVC.SelectableList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC.SingleSelectableList
{
    public class SingleSelectableListModel<ElementType, ElementData, ListView> : SelectableListModel<ElementType, ElementData, ListView>
        where ElementType : SingleSelectableListElement<ElementData>
        where ListView : SingleSelectableListView<ElementType, ElementData>
    {

       
    }
}

