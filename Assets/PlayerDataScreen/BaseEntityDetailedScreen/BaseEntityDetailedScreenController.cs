using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntityDetailedScreenController<ModelType,ViewType> : BaseController<ModelType, ViewType> where ModelType : BaseEntityDetailedScreenModel<ViewType> where ViewType : BaseEntityDetailedScreenView
{
    [field: SerializeField]
    private BaseEntityListModel EntityListModel { get; set; }

    public void SetChooseEntityCallback (Action<Entity> onEntitySelectionCallback)
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

    private void HandleOnElementSelection (Entity selectedElementData, bool isSelected)
    {
        if (isSelected == true)
        {
            CurrentModel.ShowDataOfEntity(selectedElementData);
        }
    }
}
