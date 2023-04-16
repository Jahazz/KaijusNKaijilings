using MVC.List;
using QuantumTek.QuantumQuest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDetailedDescriptionController : ListController<QuestDetailedDescriptionTaskElement, QQ_Task, QuestDetailedDescriptionView, QuestDetailedDescriptionModel>
{
    public void Initialize (QQ_Quest selectedQuest)
    {
        CurrentModel.Initialize(selectedQuest);
    }
}
