using MVC.List;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC.SelectableList
{
    public class SelectableListController<ElementType, ElementData, ListView, ListModel> : ListController<ElementType, ElementData, ListView, ListModel>
         where ElementType : SelectableListElement<ElementData>
        where ListView : SelectableListView<ElementType, ElementData>
         where ListModel : SelectableListModel<ElementType, ElementData, ListView>
    {
        
    }
}
