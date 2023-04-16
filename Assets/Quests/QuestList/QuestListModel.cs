using MVC.SingleSelectableList;
using QuantumTek.QuantumQuest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListModel : SingleSelectableListModel<QuestListElement, QQ_Quest, QuestListView>
{
    [field: SerializeField]
    private QuestDetailedDescriptionController QuestDetailedDescriptionController { get; set; }

    protected virtual void OnEnable ()
    {
        PopulateQuestList();
        OnElementSelection += HandleQuestSelection;
        CurrentView.SelectFirstElement();
    }

    protected virtual void OnDisable ()
    {
        OnElementSelection -= HandleQuestSelection;
        CurrentView.ClearList();
    }

    private void PopulateQuestList ()
    {
        foreach (KeyValuePair<int, QQ_Quest> item in SingletonContainer.Instance.QuestHandler.QuestCollection)
        {
            CurrentView.AddNewItem(item.Value);
        }
    }

    private void HandleQuestSelection (QQ_Quest selectedElementData, bool isSelected)
    {
        if (isSelected == true)
        {
            QuestDetailedDescriptionController.Initialize(selectedElementData);
        }
    }
}
