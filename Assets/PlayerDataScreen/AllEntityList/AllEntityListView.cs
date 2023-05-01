using MVC.SingleSelectableList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEntityListView : SingleSelectableListView<AllEntityListElement, StatsScriptable>
{
    [field: SerializeField]
    private AllEntityDetailedScreenController EntityDetailedScreenController { get; set; }

    public void SetActiveEntityDetailedScreenController (bool isActive)
    {
        EntityDetailedScreenController.Initialize();
        EntityDetailedScreenController.gameObject.SetActive(isActive);
    }
}
