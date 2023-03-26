using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScreenEntityController : MonoBehaviour
{
    [field: SerializeField]
    private Animator Animator { get; set; }

    public void PlayAnimation (AnimationType animationToPlay)
    {
        Animator.SetTrigger(Enum.GetName(typeof(AnimationType), animationToPlay));
        new WaitForSeconds(1.0f);
    }
}

public enum AnimationType
{
    ATTACK,
    GET_HIT,
    DIE
}
