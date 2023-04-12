using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interlocutor : MonoBehaviour
{
    [field: SerializeField]
    public Player AssignedPlayer { get; private set; }
    [field: SerializeField]
    public VIDE_Assign DialogueData { get;private set; }
    [field: SerializeField]
    public List<Interlocutor> AdditionalInterlocutors { get; set; }

    public void InitializeConversation ()
    {
        SingletonContainer.Instance.DialogueManager.Initialize(SingletonContainer.Instance.PlayerManager.CurrentPlayer, this);
    }
}
