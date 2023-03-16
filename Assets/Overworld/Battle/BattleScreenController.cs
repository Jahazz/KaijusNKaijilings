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
}
