using MVC.List;
using MVC.SingleSelectableList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityListController : SingleSelectableListController<EntityListElement, Entity, EntityListView, EntityListModel>
{
    public void ReorderEntityListByPattern (List<Entity> pattern)
    {
        CurrentModel.ReorderEntityListByPattern(pattern);
    }
}
