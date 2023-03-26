using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class BattleParticipant
{
    public Player Player { get; private set; }
    public ObservableVariable<Entity> CurrentEntity { get; private set; } = new ObservableVariable<Entity>();
    public ObservableVariable<BaseBattleAction> SelectedBattleAction { get; private set; } = new ObservableVariable<BaseBattleAction>();

    public BattleParticipant (Player player)
    {
        Player = player;
    }

    public void QueueAttackAction (Entity caster,Entity skillTarget, SkillScriptableObject skill)
    {
        SelectedBattleAction.PresentValue = new AttackBattleAction(caster, skillTarget, skill);
    }

    public void SelectFirstAliveEntity ()
    {
        CurrentEntity.PresentValue = GetFirstAliveEntity();
    }

    private Entity GetFirstAliveEntity ()
    {
        return Player.EntitiesInEquipment.First(entity => entity.IsAlive.PresentValue == true);
    }
}
