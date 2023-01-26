using MVC.SelectableList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualEntityListController : SelectableListController<RitualEntityListElement, Entity, RitualEntityListView,RitualEntityListModel>
{
    public void StartRitual ()
    {
        CurrentModel.StartRitual();
    }
}
