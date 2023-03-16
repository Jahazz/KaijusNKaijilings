using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScreenModel : BaseModel<BattleScreenView>
{
    protected override void AttachToEvents ()
    {
        BattleFactory.OnBattleCreation += HandleOnBattleCreated;
    }

    private void HandleOnBattleCreated (Battle createdBattle)
    {
        foreach (BattleParticipant battleParticipant in createdBattle.BattleParticipantsCollection)
        {
            battleParticipant.CurrentEntity.OnVariableChange += (Entity entity) => OnPlayerEntityChanged(battleParticipant.Player, entity);
            OnPlayerEntityChanged(battleParticipant.Player, battleParticipant.CurrentEntity.PresentValue);
        }
    }

    private void OnPlayerEntityChanged (Player owner, Entity newValue)
    {
        if (owner.IsNPC == false)
        {
            CurrentView.SpawnEntityInFirstPosition(newValue);
        }
        else
        {
            CurrentView.SpawnEntityInSecondPosition(newValue);
        }
    }

    protected override void DetachFromEvents ()
    {
        base.DetachFromEvents();
    }

}
