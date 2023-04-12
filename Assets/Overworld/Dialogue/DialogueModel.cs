using MVC;
using MVC.List;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using VIDE_Data;

public class DialogueModel : ListModel<DialogueResponseOption, DialogueResponseOptionData, DialogueView>
{
    private Interlocutor CurrentMainInterlocutor { get; set; }
    private Dictionary<string, Player> AdditionalInterlocutorsCollection { get; set; }

    private const string PLAYER_DEFAULT_ID = "Player";
    private const string IS_CHOICE_NODE_KEY = "IsChoice";

    public void InitializeDialogue (Interlocutor mainInterlocutor)
    {
        CurrentMainInterlocutor = mainInterlocutor;

        GenerateInterlocutorCollection();

        VD.OnActionNode += HandleOnActionNode;
        VD.OnNodeChange += HandleOnNodeChange;
        VD.OnEnd += HandleOnEnd;

        CurrentView.SetCanvasEnabled(true);

        VD.BeginDialogue(mainInterlocutor.DialogueData);
    }

    public void SelectResponse (int responseID)
    {
        VD.nodeData.commentIndex = responseID;
        VD.Next();
    }

    private void HandleOnEnd (VD.NodeData data)
    {
        throw new System.NotImplementedException();
    }

    private void HandleOnNodeChange (VD.NodeData data)
    {
        Player currentlyTalkingPlayer = AdditionalInterlocutorsCollection[data.tag];

        if (IsNodeChoiceNode(data) == true)
        {
            CurrentView.SetResponses(data.comments);
        }
        else
        {
            bool isOnLeft = data.isPlayer;
            string dialogueText = data.comments[data.commentIndex];
            CurrentView.SetDialogue(currentlyTalkingPlayer, isOnLeft, dialogueText);
        }

    }

    

    private bool IsNodeChoiceNode (VD.NodeData data)
    {
        return data.isPlayer && ( data.extraVars.ContainsKey(IS_CHOICE_NODE_KEY) == false || bool.Parse(data.extraVars[IS_CHOICE_NODE_KEY].ToString()) == true);
    }

    private void HandleOnActionNode (int nodeID)
    {
        throw new System.NotImplementedException();
    }

    private void GenerateInterlocutorCollection ()
    {
        if (CurrentMainInterlocutor.AdditionalInterlocutors!= null && CurrentMainInterlocutor.AdditionalInterlocutors.Count > 0)
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
