using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDetailedScreenController : BaseEntityDetailedScreenController<EntityDetailedScreenModel, EntityDetailedScreenView>
{
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

    public void SetButtonsVisibility(bool isSpellbookButtonShow, bool isSummonButtonShown)
    {
        CurrentView.SetButtonsVisibility(isSpellbookButtonShow, isSummonButtonShown);
    }
}
