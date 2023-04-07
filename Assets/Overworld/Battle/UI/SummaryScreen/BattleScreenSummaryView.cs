using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScreenSummaryView : BaseView
{
    [field: SerializeField]
    private GameObject VictoryLabel { get; set; }
    [field: SerializeField]
    private GameObject DefeatLabel { get; set; }
    [field: SerializeField]
    private GameObject UnresolvedLabel { get; set; }

    [field: Space]
    [field: SerializeField]
    private GameObject MainPanel { get; set; }

    public void ChangeMainLabel (BattleResultType battleResult)
    {
        VictoryLabel.SetActive(false);
        DefeatLabel.SetActive(false);
        UnresolvedLabel.SetActive(false);

        switch (battleResult)
        {
            case BattleResultType.VICTORY:
                VictoryLabel.SetActive(true);
                break;
            case BattleResultType.DEFEAT:
                DefeatLabel.SetActive(true);
                break;
            case BattleResultType.UNRESOLVED:
                UnresolvedLabel.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void SetPanelVisibility (bool isVisible)
    {
        MainPanel.SetActive(isVisible);
    }
}
