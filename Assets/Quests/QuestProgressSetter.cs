using QuantumTek.QuantumQuest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestProgressSetter : MonoBehaviour
{
    [field: SerializeField]
    private QQ_QuestSO QuestToProgress { get; set; }
    [field: SerializeField]
    private int TaskToProgressID { get; set; }
    [field: SerializeField]
    private float AmountToProgress { get; set; }

    public void ProgressQuest ()
    {
        SingletonContainer.Instance.QuestHandler.ProgressTask(QuestToProgress.Quest.ID, TaskToProgressID, AmountToProgress);
    }
}
