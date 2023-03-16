using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle
{
    public List<BattleParticipant> BattleParticipantsCollection { get; private set; } = new List<BattleParticipant>();

    public Battle (List<Player> participantsCollection)
    {
        foreach (Player playerParticipant in participantsCollection)
        {
            BattleParticipantsCollection.Add(new BattleParticipant(playerParticipant));
        }
    }

    public void StartBattle ()
    {

    }
}
