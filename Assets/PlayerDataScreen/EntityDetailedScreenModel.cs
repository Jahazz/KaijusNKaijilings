using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDetailedScreenModel : BaseModel<EntityDetailedScreenView>
{
    public Entity CurrentEntity { get; private set; }
    private Action<Entity> OnEntitySelectionCallback { get; set; }

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
        OnEntitySelectionCallback?.Invoke(CurrentEntity);
        SingletonContainer.Instance.CharacterMenuController.HideCharacterMenu();
    }

    public void SetChooseEntityCallback (Action<Entity> onEntitySelectionCallback)
    {
        OnEntitySelectionCallback = onEntitySelectionCallback;
    }
}
