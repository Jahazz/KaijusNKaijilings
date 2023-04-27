using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MVC.SingleSelectableList;
using System;

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
    [field: SerializeField]
    private SwirlBackgroundAnimator SwirlAnimator { get; set; }

    public void ToggleMenuVisibility(bool isMenuVisible)
    {
        MainMenuParent.SetActive(isMenuVisible);
        ShowSelectEntityLabel(false);
        EntityScreenController.SetButtonsVisibility(true, false);

        if (isMenuVisible == true)
        {
            SwirlAnimator.Show();
        }
        else
        {
            SwirlAnimator.Hide();
        }
    }

    public override CharacterMenuElement AddNewItem (CharacterMenuElementData elementData)
    {
        ContainingElementsCollection.Add(elementData, elementData.CharacterMenuElement);
        elementData.CharacterMenuElement.Initialize(elementData);
        elementData.CharacterMenuElement.InitializeOnSelectionAction(HandleOnElementSelection);
        return elementData.CharacterMenuElement;
    }

    public void OpenMenuAsEntitySelection(Action<Entity> onEntitySelectionCallback)
    {
        ContainingElementsCollection.Where(n => n.Key.TabType == CharacterMenuTabType.ENTITIES).FirstOrDefault().Value.Select();
        ShowSelectEntityLabel(true);
        EntityScreenController.SetChooseEntityCallback(onEntitySelectionCallback);
    }

    private void ShowSelectEntityLabel(bool isShown)
    {
        TabsContainer.SetActive(isShown == false);
        SelectEntityLabel.SetActive(isShown);
        EntityScreenController.SetButtonsVisibility(false, true);
    }
}
