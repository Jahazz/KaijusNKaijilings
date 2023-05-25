using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntityDetailedScreenModel<ViewType> : BaseModel<ViewType> where ViewType : BaseEntityDetailedScreenView
{
    private Action<Entity> OnEntitySelectionCallback { get; set; }

    public Entity CurrentEntity { get; private set; }

    public virtual void ShowDataOfEntity (Entity targetEntity)
    {
        CurrentEntity = targetEntity;

        CurrentView.SetData(CurrentEntity);
    }

    public void SetChooseEntityCallback (Action<Entity> onEntitySelectionCallback)
    {
        OnEntitySelectionCallback = onEntitySelectionCallback;
    }

    public void SetCurrentBattleEntityToThisAndCloseWindow ()
    {
        OnEntitySelectionCallback?.Invoke(CurrentEntity);
        SingletonContainer.Instance.CharacterMenuController.HideCharacterMenu(false);
    }

}
