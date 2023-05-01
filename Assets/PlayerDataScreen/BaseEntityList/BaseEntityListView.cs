using MVC.SingleSelectableList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntityListView : SingleSelectableListView<BaseEntityListElement, Entity>
{
    [field: SerializeField]
    private EntityDetailedScreenController EntityDetailedScreenController { get; set; }

    public void SetActiveEntityDetailedScreenController (bool isActive)
    {
        EntityDetailedScreenController.Initialize();
        EntityDetailedScreenController.gameObject.SetActive(isActive);
    }
}
