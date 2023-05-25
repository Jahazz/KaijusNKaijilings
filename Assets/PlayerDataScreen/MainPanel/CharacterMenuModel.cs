using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVC.SingleSelectableList;
using System;

public class CharacterMenuModel : SingleSelectableListModel<CharacterMenuElement, CharacterMenuElementData, CharacterMenuView>
{
    [field: SerializeField]
    private List<CharacterMenuElementData> MenuElementCollection { get; set; }
    [field: SerializeField]
    private ObjectToCanvasRaycaster BookController { get; set; }
    [field: SerializeField]
    private Camera BookCamera { get; set; }

    public override void Initialize (CharacterMenuView currentView)
    {
        base.Initialize(currentView);

        foreach (CharacterMenuElementData item in MenuElementCollection)
        {
            CurrentView.AddNewItem(item);
        }

        OnElementSelection += HandleElementSelection;
        BookController.OnBookPrepared += HandleOnBookPrepared;
        CurrentView.SelectFirstElement();
    }

    private void HandleOnBookPrepared ()
    {
        ToggleMenuVisibility(false);
    }

    public void ToggleMenuVisibility (bool isMenuVisible, bool unfreezePlayer = true)
    {
        if (isMenuVisible == true)
        {
            SingletonContainer.Instance.OverworldPlayerCharacterManager.FreezePlayer(PlayerState.IN_MENU);
        }
        else
        {
            if (unfreezePlayer == true)
            {
                SingletonContainer.Instance.OverworldPlayerCharacterManager.UnfreezePlayer();
            }
        }

        BookCamera.gameObject.SetActive(isMenuVisible);
        CurrentView.ToggleMenuVisibility(isMenuVisible);
    }

    public void OpenMenuAsEntitySelection(Action<Entity> onEntitySelectionCallback)
    {
        CurrentView.OpenMenuAsEntitySelection(onEntitySelectionCallback);
    }

    protected override void DetachFromEvents ()
    {
        base.DetachFromEvents();
        OnElementSelection -= HandleElementSelection;
    }

    private void HandleElementSelection (CharacterMenuElementData selectedElementData, bool isSelected)
    {
        if (isSelected == true)
        {
            BookController.OpenPage(selectedElementData.PageIndex);
        }
    }
}
