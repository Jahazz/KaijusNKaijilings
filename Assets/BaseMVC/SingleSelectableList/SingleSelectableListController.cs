using MVC.List;
using MVC.SelectableList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC.SingleSelectableList
{
    public class SingleSelectableListController<ElementType, ElementData, ListView, ListModel> : SelectableListController<ElementType, ElementData, ListView, ListModel>
         where ElementType : SingleSelectableListElement<ElementData>
        where ListView : SingleSelectableListView<ElementType, ElementData>
         where ListModel : SingleSelectableListModel<ElementType, ElementData, ListView>
    {
        
    }
}
