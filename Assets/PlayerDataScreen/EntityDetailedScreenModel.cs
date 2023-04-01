using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDetailedScreenModel : BaseModel<EntityDetailedScreenView>
{
    public Entity CurrentEntity { get; private set; }

    public void ShowDataOfEntity (Entity targetEntity)
    {
        CurrentEntity = targetEntity;

        CurrentView.SetData(CurrentEntity);
    }

    public void ChangeEntityCustomName (string value)
    {
        CurrentEntity.Name.PresentValue = value;
    }

    public void SetCurrentBattleEntityToThisAndCloseWindow ()
    {
        SingletonContainer.Instance.BattleScreenManager.BattleScreenController.ChangeCurrentEntity(CurrentEntity);
        SingletonContainer.Instance.CharacterMenuController.HideCharacterMenu();
    }
}
