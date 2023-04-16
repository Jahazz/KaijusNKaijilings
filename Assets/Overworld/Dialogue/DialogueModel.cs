using MVC;
using MVC.List;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Ink.Runtime;

public class DialogueModel : ListModel<DialogueResponseOption, DialogueResponseOptionData, DialogueView>
{
    private Interlocutor CurrentMainInterlocutor { get; set; }
    private Dictionary<string, Player> AdditionalInterlocutorsCollection { get; set; }
    private Story CurrentStory { get; set; }

    private const string PLAYER_DEFAULT_ID = "Player";
    private const string IS_CHOICE_NODE_KEY = "IsChoice";

    public void InitializeDialogue (Interlocutor mainInterlocutor)
    {
        CurrentMainInterlocutor = mainInterlocutor;

        GenerateInterlocutorCollection();

        CurrentStory = mainInterlocutor.GenerateStory();
        //VD.OnActionNode += HandleOnActionNode;
        //VD.OnNodeChange += HandleOnNodeChange;
        //VD.OnEnd += HandleOnEnd;

        CurrentView.SetCanvasEnabled(true);

        ContinueDialogue();
        //VD.BeginDialogue(mainInterlocutor.DialogueData);
    }

    public void SelectResponse (int responseID)
    {
        CurrentStory.ChooseChoiceIndex(responseID);
        ContinueDialogue();
    }

    public void SpeedupOrProgress ()
    {
        if (CurrentView.IsAnyDialogueMidAnimating() == true)
        {
            CurrentView.SkipDialogueAnimation();
        }
        else
        {
            ContinueDialogue();
        }
    }

    private void HandleOnEnd ()
    {
        //VD.EndDialogue();
        CurrentView.SetCanvasEnabled(false);
        CurrentView.SkipDialogueAnimation();
        SingletonContainer.Instance.DialogueManager.Close();
    }

    private void ContinueDialogue ()
    {
        CurrentStory.Continue();
        bool isChoice = CurrentStory.currentChoices.Count > 0;
        string text = CurrentStory.currentText;
        InkNodeTagsData nodeTags = new InkNodeTagsData(CurrentStory.currentTags);
        CurrentView.SetProceedButtonEnabled(isChoice == false);

        if (nodeTags.SpeakerID != null)
        {
            Player currentlyTalkingPlayer = AdditionalInterlocutorsCollection[nodeTags.SpeakerID];

            CurrentView.SetDialogue(currentlyTalkingPlayer, nodeTags.SpeakerSide == Side.LEFT, text);
            CurrentView.ClearList();
        }
        else if (isChoice == true)
        {
            string[] choiceCollection = CurrentStory.currentChoices.Select(x => x.text.Trim()).ToArray();
            CurrentView.SetResponses(choiceCollection);
        }
        else
        {
            ContinueDialogue();
        }


        if (isChoice == false && CurrentStory.canContinue == false)
        {
            HandleOnEnd();
        }
    }

    private void GenerateInterlocutorCollection ()
    {
        if (CurrentMainInterlocutor.AdditionalInterlocutors != null && CurrentMainInterlocutor.AdditionalInterlocutors.Count > 0)
        {
            AdditionalInterlocutorsCollection = CurrentMainInterlocutor.AdditionalInterlocutors.ToDictionary(x => x.AssignedPlayer.Name, x => x.AssignedPlayer);
        }
        else
        {
            AdditionalInterlocutorsCollection = new Dictionary<string, Player>();
        }

        AdditionalInterlocutorsCollection.Add(CurrentMainInterlocutor.AssignedPlayer.Name, CurrentMainInterlocutor.AssignedPlayer);
        AdditionalInterlocutorsCollection.Add(PLAYER_DEFAULT_ID, SingletonContainer.Instance.PlayerManager.CurrentPlayer);
    }
}
