using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MVC.SingleSelectableList;

public class CharacterMenuView : SingleSelectableListView<CharacterMenuElement, CharacterMenuElementData>
{
    [field: SerializeField]
    private GameObject MainMenuParent { get; set; }
    [field: SerializeField]
    private GameObject TabsContainer { get; set; }
    [field: SerializeField]
    private GameObject SelectEntityLabel { get; set; }
    [field: SerializeField]
    private EntityDetailedScreenController EntityScreenController { get; set; }

    public void ToggleMenuVisibility(bool isMenuVisible)
    {
        MainMenuParent.SetActive(isMenuVisible);
        ShowSelectEntityLabel(false);
        EntityScreenController.SetButtonsVisibility(true, false);
    }

    public override CharacterMenuElement AddNewItem (CharacterMenuElementData elementData)
    {
        ContainingElementsCollection.Add(elementData, elementData.CharacterMenuElement);
        elementData.CharacterMenuElement.Initialize(elementData);
        elementData.CharacterMenuElement.InitializeOnSelectionAction(HandleOnElementSelection);
        return elementData.CharacterMenuElement;
    }

    public void OpenMenuAsEntitySelection()
    {
        ContainingElementsCollection.Where(n => n.Key.TabType == CharacterMenuTabType.ENTITIES).FirstOrDefault().Value.Select();
        ShowSelectEntityLabel(true);
        //TODO: show tab as "Select entity to summon", toggle button to summon entity(disable after close) on button click disable,
        // cleanup, change entity in battle.    
    }

    private void ShowSelectEntityLabel(bool isShown)
    {
        TabsContainer.SetActive(isShown == false);
        SelectEntityLabel.SetActive(isShown);
        EntityScreenController.SetButtonsVisibility(false, true);
    }
}
