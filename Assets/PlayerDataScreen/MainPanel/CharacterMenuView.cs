using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVC.SingleSelectableList;

public class CharacterMenuView : SingleSelectableListView<CharacterMenuElement, CharacterMenuElementData>
{
    [field: SerializeField]
    private GameObject MainMenuParent { get; set; }

    public void ToggleMenuVisibility(bool isMenuVisible)
    {
        MainMenuParent.SetActive(isMenuVisible);
    }

    public override CharacterMenuElement AddNewItem (CharacterMenuElementData elementData)
    {
        ContainingElementsCollection.Add(elementData, elementData.CharacterMenuElement);
        elementData.CharacterMenuElement.Initialize(elementData);
        elementData.CharacterMenuElement.InitializeOnSelectionAction(HandleOnElementSelection);
        return elementData.CharacterMenuElement;
    }
}
