using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVC.SingleSelectableList;

public class CharacterMenuModel : SingleSelectableListModel<CharacterMenuElement, CharacterMenuElementData, CharacterMenuView>
{
    [field: SerializeField]
    private List<CharacterMenuElementData> MenuElementCollection { get; set; }

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

        CurrentView.ToggleMenuVisibility(isMenuVisible);
    }

    protected override void DetachFromEvents ()
    {
        base.DetachFromEvents();
        OnElementSelection -= HandleElementSelection;
    }

    private void HandleElementSelection (CharacterMenuElementData selectedElementData, bool isSelected)
    {
        selectedElementData.BoundPanel.SetActive(isSelected);
    }
}
