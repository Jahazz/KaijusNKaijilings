using MVC.SingleSelectableList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEntityListController : SingleSelectableListController<AllEntityListElement, StatsScriptable, AllEntityListView, AllEntityListModel>
{

    protected override void Awake ()
    {
        base.Awake();
    }
}
