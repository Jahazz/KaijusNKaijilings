using MVC.List;
using QuantumTek.QuantumQuest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDetailedDescriptionModel : ListModel<QuestDetailedDescriptionTaskElement, QQ_Task, QuestDetailedDescriptionView>
{
    private QQ_Quest CurrentQuest { get; set; }

    public void Initialize (QQ_Quest currentQuest)
    {
        CurrentQuest = currentQuest;

        CurrentView.FillQuestData(CurrentQuest);

        CurrentView.ClearList();

        foreach (QQ_Task task in CurrentQuest.Tasks)
        {
            CurrentView.AddNewItem(task);
        }
    }
}
