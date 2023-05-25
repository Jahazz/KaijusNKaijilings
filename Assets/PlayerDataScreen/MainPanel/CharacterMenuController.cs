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
        if (SingletonContainer.Instance.OverworldPlayerCharacterManager.CurrentPlayerState == PlayerState.IN_OVERWORLD)
        {
            OpenCharacterMenu();
        }
    }

    public void OpenMenuAsEntitySelection(Action<Entity> onEntitySelectionCallback)
    {
        OpenCharacterMenu();
        CurrentModel.OpenMenuAsEntitySelection( onEntitySelectionCallback);
    }

    public void HideCharacterMenu(bool unfreezePlayer = true)
    {
        IsMenuVisible = false;
        CurrentModel.ToggleMenuVisibility(false, unfreezePlayer);
    }

    private void OpenCharacterMenu()
    {
        IsMenuVisible = !IsMenuVisible;
        CurrentModel.ToggleMenuVisibility(IsMenuVisible);
    }
}
