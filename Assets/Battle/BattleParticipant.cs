using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class BattleParticipant
{
    public Player Player { get; private set; }
    public ObservableVariable<Entity> CurrentEntity { get; private set; } = new ObservableVariable<Entity>();

    public BattleParticipant (Player player)
    {
        Player = player;
        CurrentEntity.PresentValue = GetFirstAliveEntity();
    }

    private Entity GetFirstAliveEntity ()
    {
        return Player.EntitiesInEquipment.First(entity => entity.IsAlive() == true);
    }
}
