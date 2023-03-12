using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battler : MonoBehaviour
{
    [field: SerializeField]
    private Animator ActorAnimator { get; set; }

    public void InitialzieBattle ()
    {
        SingletonContainer.Instance.BattleScreenManager.Initialize(SingletonContainer.Instance.PlayerManager.PlayerOverworldAnimator, ActorAnimator);
    }
}
