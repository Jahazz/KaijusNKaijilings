using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScreenSummaryModel : BaseModel<BattleScreenSummaryView>
{
    BattleResultType BattleResult { get; set; }

    public void OpenScreen (BattleResultType battleResult)
    {
        BattleResult = battleResult;
        CurrentView.SetPanelVisibility(true);
        CurrentView.ChangeMainLabel(battleResult);
    }

    public void CloseScreen ()
    {
        CurrentView.SetPanelVisibility(false);
        SingletonContainer.Instance.BattleScreenManager.BattleScreenController.CleanupBattle();
        SingletonContainer.Instance.BattleScreenManager.Close(BattleResult);
    }
}
