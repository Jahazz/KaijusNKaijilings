using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBattleAction
{
    public BattleActionType ActionType { get; protected set; }

    public virtual void Invoke (BattleParticipant actionOwner, Battle battle)
    {

    }

}
