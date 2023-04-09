using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDetailedScreenController : BaseController<EntityDetailedScreenModel, EntityDetailedScreenView>
{
    [field: SerializeField]
    private EntityListModel EntityListModel { get; set; }
    [field: SerializeField]
    private SkillListController SkillListController { get; set; }

    public void ChangeEntityCustomName (string newName)
    {
        CurrentModel.ChangeEntityCustomName(newName);
    }

    public void ShowSpellbook ()
    {
        SkillListController.Show(CurrentModel.CurrentEntity);
    }

    public void SummonEntity ()
    {
        CurrentModel.SetCurrentBattleEntityToThisAndCloseWindow();
    }

    public void SetChooseEntityCallback (Action<Entity> onEntitySelectionCallback)
    {
        CurrentModel.SetChooseEntityCallback(onEntitySelectionCallback);
    }

    public void SetButtonsVisibility(bool isSpellbookButtonShow, bool isSummonButtonShown)
    {
        CurrentView.SetButtonsVisibility(isSpellbookButtonShow, isSummonButtonShown);
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
