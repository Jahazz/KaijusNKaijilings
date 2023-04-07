using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVC.SingleSelectableList;
using System;

public class CharacterMenuController : SingleSelectableListController<CharacterMenuElement, CharacterMenuElementData, CharacterMenuView, CharacterMenuModel>
{
    private bool IsMenuVisible { get; set; }

    public void HandleOnMenuButtonClick ()
    {
        if (SingletonContainer.Instance.BattleScreenManager.BattleScreenController.IsInBattle() == false)
        {
            OpenCharacterMenu();
        }
    }

    public void OpenMenuAsEntitySelection(Action<Entity> onEntitySelectionCallback)
    {
        OpenCharacterMenu();
        CurrentModel.OpenMenuAsEntitySelection( onEntitySelectionCallback);
    }

    public void HideCharacterMenu()
    {
        IsMenuVisible = false;
        CurrentModel.ToggleMenuVisibility(false);
    }

    private void OpenCharacterMenu()
    {
        IsMenuVisible = !IsMenuVisible;
        CurrentModel.ToggleMenuVisibility(IsMenuVisible);
    }
}
