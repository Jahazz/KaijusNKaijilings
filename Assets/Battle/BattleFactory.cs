using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleFactory
{
    public delegate void BattleArguments (Battle createdBattle);
    public static event Action<Battle> OnBattleCreation;

    public static Battle CurrentBattle { get; private set; }

    public static void StartNewBattle (Player firstParticipant, Player secondParticipant)
    {
        CurrentBattle = new Battle(new List<Player> { firstParticipant, secondParticipant });
        OnBattleCreation.Invoke(CurrentBattle);
    }
}
