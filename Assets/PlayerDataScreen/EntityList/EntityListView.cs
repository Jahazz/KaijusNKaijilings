using MVC.List;
using MVC.SingleSelectableList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityListView : BaseEntityListView
{
    [field: SerializeField]
    private EntityDetailedScreenController EntityDetailedScreenController { get; set; }

    public void SetActiveEntityDetailedScreenController (bool isActive)
    {
        EntityDetailedScreenController.Initialize();
        EntityDetailedScreenController.gameObject.SetActive(isActive);
    }
}
