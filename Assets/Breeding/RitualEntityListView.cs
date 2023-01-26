using MVC.SelectableList;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RitualEntityListView : SelectableListView<RitualEntityListElement, Entity>
{
    [field: SerializeField]
    private TMP_Text AcceptButtonLabel { get; set; }
    [field: SerializeField]
    private Button AcceptButton { get; set; }
    [field: SerializeField]
    private List<RangeSliderStatPair> RangeSliderStatPairCollection { get; set; }
    [field: SerializeField]
    private RitualResultScreen ResultScreen { get; set; }

    public void UpdateResultStats (BaseStatsData<Vector2> expectedResultStatRange, BaseStatsData<Vector2> defaultStatRange)
    {
        foreach (RangeSliderStatPair pair in RangeSliderStatPairCollection)
        {
            Vector2 currentDefaultStatRange = defaultStatRange.GetStatOfType(pair.StatType);
            Vector2 currentExpectedStatRange = expectedResultStatRange.GetStatOfType(pair.StatType);

            pair.RangeSlider.SetValue(currentDefaultStatRange.x, currentDefaultStatRange.y, currentExpectedStatRange.x, currentExpectedStatRange.y);
        }
    }

    public void SetAcceptButtonText (string text)
    {
        AcceptButtonLabel.text = text;
    }

    public void SetAcceptButtonInteractable(bool isInteractable)
    {
        AcceptButton.interactable =isInteractable;
    }

    public void ShowSummonedEntity (Entity newlyCreatedEntity)
    {
        ResultScreen.Show(newlyCreatedEntity);
    }

}
