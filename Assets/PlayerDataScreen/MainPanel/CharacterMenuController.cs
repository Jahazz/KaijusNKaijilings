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
        PlayerState currentPlayerState = SingletonContainer.Instance.OverworldPlayerCharacterManager.CurrentPlayerState;
        if (currentPlayerState == PlayerState.IN_OVERWORLD || currentPlayerState == PlayerState.IN_MENU)
        {
            OpenCharacterMenu();
        }
    }

    public void OpenMenuAsEntitySelection (Action<Entity> onEntitySelectionCallback)
    {
        OpenCharacterMenu();
        CurrentModel.OpenMenuAsEntitySelection(onEntitySelectionCallback);
    }

    public void HideCharacterMenu (bool unfreezePlayer = true)
    {
        IsMenuVisible = false;
        CurrentModel.ToggleMenuVisibility(false, unfreezePlayer);
    }

    private void OpenCharacterMenu ()
    {
        IsMenuVisible = !IsMenuVisible;
        CurrentModel.ToggleMenuVisibility(IsMenuVisible);
    }
}
