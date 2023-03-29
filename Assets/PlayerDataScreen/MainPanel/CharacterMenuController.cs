using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVC.SingleSelectableList;

public class CharacterMenuController : SingleSelectableListController<CharacterMenuElement, CharacterMenuElementData, CharacterMenuView, CharacterMenuModel>
{
    private bool IsMenuVisible { get; set; }

    public void HandleOnMenuButtonClick ()
    {
        IsMenuVisible = !IsMenuVisible;

        CurrentModel.ToggleMenuVisibility(IsMenuVisible);
    }
}
