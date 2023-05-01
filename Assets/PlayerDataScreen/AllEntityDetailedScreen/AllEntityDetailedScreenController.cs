using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEntityDetailedScreenController : BaseController<AllEntityDetailedScreenModel, AllEntityDetailedScreenView>
{
    [field: SerializeField]
    private AllEntityListModel EntityListModel { get; set; }

    public void SetChooseEntityCallback (Action<StatsScriptable> onEntitySelectionCallback)
    {
        CurrentModel.SetChooseEntityCallback(onEntitySelectionCallback);
    }

    public void Initialize ()
    {
        CurrentModel.Initialize(CurrentView);
        EntityListModel.OnElementSelection += HandleOnElementSelection;
    }

    protected override void DetachFromEvents ()
    {
        base.DetachFromEvents();
        EntityListModel.OnElementSelection -= HandleOnElementSelection;
    }

    private void HandleOnElementSelection (StatsScriptable selectedElementData, bool isSelected)
    {
        if (isSelected == true)
        {
            CurrentModel.ShowDataOfEntity(selectedElementData);
        }
    }
}
