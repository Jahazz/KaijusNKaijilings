using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScreenSummaryModel : BaseModel<BattleScreenSummaryView>
{
    public void OpenScreen (BattleResultType battleResult)
    {
        CurrentView.SetPanelVisibility(true);
        CurrentView.ChangeMainLabel(battleResult);
    }

    public void CloseScreen ()
    {
        CurrentView.SetPanelVisibility(false);
        SingletonContainer.Instance.BattleScreenManager.BattleScreenController.CleanupBattle();
        SingletonContainer.Instance.BattleScreenManager.Close();
    }
}
