using QuantumTek.QuantumQuest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStarter : MonoBehaviour
{
    [field: SerializeField]
    private QQ_QuestSO QuestToStart { get; set; }

    //public void StartQuest ()
    //{
    //    SingletonContainer.Instance.QuestHandler.AssignQuest(QuestToStart.Quest.Name);
    //}

    //public void StartQuestByName (string questName)
    //{
    //    SingletonContainer.Instance.QuestHandler.AssignQuest(questName);
    //}
}
