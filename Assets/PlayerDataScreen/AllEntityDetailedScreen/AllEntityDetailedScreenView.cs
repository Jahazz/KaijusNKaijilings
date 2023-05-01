using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections.ObjectModel;
using UnityEngine.UI;

public class AllEntityDetailedScreenView : BaseView
{
    [field: SerializeField]
    private TMP_Text EntityName { get; set; }
    [field: SerializeField]
    private TMP_Text EntityDescription { get; set; }
    [field: SerializeField]
    private Image EntityImage { get; set; }
    [field: SerializeField]
    private TypeListController TypeList { get; set; }
    [field: SerializeField]
    private StatRangedSpiderGraph StatGraph { get; set; }

    public void SetPersistentData (string name, string description,Sprite sprite, List<TypeDataScriptable> types)
    {
        EntityImage.sprite = sprite;
        EntityName.text = name;
        EntityDescription.text = description;
        
        TypeList.Initialize(new ObservableCollection<TypeDataScriptable>(types));
    }

    public void SetStats (BaseStatsData<Vector2> expectedResultStatRange, BaseStatsData<Vector2> defaultStatRange, StatType[] statTypesCollection)
    {
        foreach (StatType statType in statTypesCollection)
        {

            Vector2 currentDefaultStatRange = defaultStatRange.GetStatOfType(statType);
            Vector2 currentExpectedStatRange = expectedResultStatRange.GetStatOfType(statType);
            StatGraph.UpdateNode(statType, currentDefaultStatRange, currentExpectedStatRange);
        }
    }
}
