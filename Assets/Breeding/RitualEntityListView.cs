using MVC.SelectableList;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    private RitualResultScreen ResultScreen { get; set; }
    [field: SerializeField]
    private TraitListController ExpectedTraitList { get; set; }
    [field: SerializeField]
    private StatRangedSpiderGraph SpiderGraph { get; set; }

    public void UpdateResultStats (BaseStatsData<Vector2> expectedResultStatRange, BaseStatsData<Vector2> defaultStatRange)
    {
        //new sexy spider graph
        StatType[] statTypesCollection = { StatType.MIGHT, StatType.MAGIC, StatType.WILLPOWER, StatType.AGILITY, StatType.INITIATIVE };

        foreach (StatType statType in statTypesCollection)
        {
            
            Vector2 currentDefaultStatRange = defaultStatRange.GetStatOfType(statType);
            Vector2 currentExpectedStatRange = expectedResultStatRange.GetStatOfType(statType);
            SpiderGraph.UpdateNode(statType, currentDefaultStatRange, currentExpectedStatRange);
        }
    }

    public void GenerateExpectedTraitList (ObservableCollection<TraitBaseScriptableObject> availableTraits)
    {
        ExpectedTraitList.InitializeTraits(availableTraits);
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
