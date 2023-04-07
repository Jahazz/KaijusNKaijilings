using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScreenSummaryController : BaseController<BattleScreenSummaryModel, BattleScreenSummaryView>
{
    public void OpenScreen (BattleResultType battleResult)
    {
        CurrentModel.OpenScreen(battleResult);
    }

    public void CloseScreen ()
    {
        CurrentModel.CloseScreen();
    }
}
