using MVC.SingleSelectableList;
using QuantumTek.QuantumQuest;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestListElement : SingleSelectableListElement<QQ_Quest>
{
    [field: SerializeField]
    private TMP_Text QuestNameLabel { get; set; }

    public override void Initialize (QQ_Quest elementData)
    {
        base.Initialize(elementData);

        QuestNameLabel.text = elementData.Name;
    }
}
