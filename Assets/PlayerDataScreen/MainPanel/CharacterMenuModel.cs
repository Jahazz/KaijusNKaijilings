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
    [field: SerializeField]
    private Camera CharacterCamera { get; set; }

    public override void Initialize (CharacterMenuView currentView)
    {
        base.Initialize(currentView);

        foreach (CharacterMenuElementData item in MenuElementCollection)
        {
            CurrentView.AddNewItem(item);
        }

        OnElementSelection += HandleElementSelection;
        CurrentView.SelectFirstElement();
    }

    public void ToggleMenuVisibility (bool isMenuVisible)
    {
        if (isMenuVisible == true)
        {
            SingletonContainer.Instance.OverworldPlayerCharacterManager.FreezePlayer();
        }
        else
        {
            SingletonContainer.Instance.OverworldPlayerCharacterManager.UnfreezePlayer();
        }

        BookCamera.gameObject.SetActive(isMenuVisible);
        CharacterCamera.gameObject.SetActive(!isMenuVisible);
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
