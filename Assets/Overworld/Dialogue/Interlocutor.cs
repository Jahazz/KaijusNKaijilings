using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interlocutor : MonoBehaviour
{
    [field: SerializeField]
    private Player ActorAnimator { get; set; }

    public void InitializeConversation ()
    {
        SingletonContainer.Instance.DialogueManager.Initialize(SingletonContainer.Instance.PlayerManager.CurrentPlayer, ActorAnimator);
    }
}
