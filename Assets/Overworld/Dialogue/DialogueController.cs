using MVC;
using MVC.List;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : ListController<DialogueResponseOption, DialogueResponseOptionData, DialogueView, DialogueModel>
{
    public void InitializeDialogue (Interlocutor mainInterlocutor)
    {
        CurrentModel.InitializeDialogue(mainInterlocutor);
    }

    public void ProceedDialogue ()
    {

    }

    public void SelectResponse(int responseID)
    {
        CurrentModel.SelectResponse(responseID);
    }
}
