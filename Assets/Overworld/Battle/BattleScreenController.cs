using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScreenController : BaseController<BattleScreenModel, BattleScreenView>
{
    public void InitializeBattle (Player firstPlayer, Player secondPlayer)
    {
        BattleFactory.StartNewBattle(firstPlayer, secondPlayer);
    }

    public void UseSkill (Entity caster, SkillScriptableObject skill)
    {
        CurrentModel.QueuePlayerSkillUsage(caster, skill);
    }

    public void ChangeCurrentEntity(Entity entity)
    {
        CurrentModel.QueuePlayerEntitySwap(entity);
    }

    public bool IsInBattle()
    {
        return CurrentModel.IsInBattle();
    }
}
