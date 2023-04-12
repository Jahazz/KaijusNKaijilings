using MVC;
using MVC.List;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueView : ListView<DialogueResponseOption, DialogueResponseOptionData>
{
    [field: SerializeField]
    private CharacterDialogue LeftDialogueWindow { get; set; }
    [field: SerializeField]
    private CharacterDialogue RightDialogueWindow { get; set; }
    [field: SerializeField]
    private Canvas DialogueCanvas { get; set; }
    [field: SerializeField]
    private Button ProceedDialogueButton { get; set; }

    public void SetCanvasEnabled (bool isEnabled)
    {
        DialogueCanvas.gameObject.SetActive(isEnabled);
        LeftDialogueWindow.gameObject.SetActive(false);
        RightDialogueWindow.gameObject.SetActive(false);
    }

    public void SetProceedButtonEnabled(bool isEnabled)
    {
        ProceedDialogueButton.gameObject.SetActive(isEnabled);
    }

    public void SkipDialogueAnimation ()
    {
        LeftDialogueWindow.SkipAnimation();
        RightDialogueWindow.SkipAnimation();
    }

    public void SetDialogue (Player author, bool isOnLeftSide, string text)
    {
        CharacterDialogue windowToPopulateText = isOnLeftSide == true ? LeftDialogueWindow : RightDialogueWindow;
        windowToPopulateText.gameObject.SetActive(true);
        windowToPopulateText.SetTextContents(author.Name, text);
    }

    public void SetResponses (string[] responsesCollection)
    {
        DialogueResponseOptionData currentResponseOptionData;

        for (int i = 0; i < responsesCollection.Length; i++)
        {
            currentResponseOptionData = new DialogueResponseOptionData(responsesCollection[i], i);
            AddNewItem(currentResponseOptionData);
        }
    }
}
