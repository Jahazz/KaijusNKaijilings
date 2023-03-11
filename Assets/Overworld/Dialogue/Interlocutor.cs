using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interlocutor : MonoBehaviour
{
    [field: SerializeField]
    private Animator ActorAnimator { get; set; }

    public void InitializeConversation ()
    {
        SingletonContainer.Instance.DialogueManager.Initialize(SingletonContainer.Instance.PlayerManager.PlayerOverworldAnimator, ActorAnimator);
    }
}
