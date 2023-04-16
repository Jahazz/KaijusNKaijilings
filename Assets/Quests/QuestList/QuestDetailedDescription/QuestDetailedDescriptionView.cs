using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVC.List;
using QuantumTek.QuantumQuest;
using TMPro;

public class QuestDetailedDescriptionView : ListView<QuestDetailedDescriptionTaskElement, QQ_Task>
{
    [field: SerializeField]
    private TMP_Text QuestNameLabel { get; set; }
    [field: SerializeField]
    private TMP_Text QuestGiverNameLabel { get; set; }
    [field: SerializeField]
    private TMP_Text QuestDescriptionLabel { get; set; }

    public void FillQuestData (QQ_Quest selectedQuest)
    {
        QuestNameLabel.text = selectedQuest.Name;
        QuestGiverNameLabel.text = selectedQuest.NPCName;
        QuestDescriptionLabel.text = selectedQuest.Description;
    }
}
