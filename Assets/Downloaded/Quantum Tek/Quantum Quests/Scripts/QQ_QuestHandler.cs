using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QuantumTek.QuantumQuest
{
    /// <summary>
    /// QQ_QuestHandler keeps track of a character's current quests.
    /// </summary>
    [AddComponentMenu("Quantum Tek/Quantum Quests/Quest Handler")]
    [DisallowMultipleComponent]
    public class QQ_QuestHandler : MonoBehaviour
    {
        [field: SerializeField]
        public QQ_QuestDB QuestsDatabase { get; set; }
        public Dictionary<int, QQ_Quest> QuestCollection = new Dictionary<int, QQ_Quest>();

        public QQ_Quest GetQuestOfId (int id)
        {
            return QuestsDatabase.Quests.Where(x => x.Quest.ID == id).Select(x => x.Quest).FirstOrDefault();
        }

        public void AssignQuest (int id)
        {
            if (!QuestCollection.ContainsKey(id))
            {
                QQ_Quest quest = new QQ_Quest(GetQuestOfId(id));
                quest.Status = QQ_QuestStatus.Inactive;
                QuestCollection.Add(id, quest);
            }
        }

        public QQ_Quest GetQuest (int id)
        {
            if (QuestCollection.ContainsKey(id))
                return QuestCollection[id];
            return null;
        }

        public QQ_Task GetTask (int questID, string taskName)
        {
            return QuestCollection[questID].GetTask(taskName);
        }

        public QQ_Task GetTask (int questID, int id)
        {
            QQ_Task output = null;

            if (QuestCollection.ContainsKey(questID))
            {
                output = QuestCollection[questID].GetTask(id);
            }

            return output;
        }

        public void ProgressTask (int questID, string taskName, float amount)
        {
            QuestCollection[questID].ProgressTask(taskName, amount);
        }

        public void ProgressTask (int questID, int taskID, float amount)
        {
            GetTask(questID, taskID)?.IncreaseProgress(amount);
        }

        public void CompleteTask (int questId, string taskName)
        {
            QuestCollection[questId].CompleteTask(taskName);
        }

        public void CompleteQuest (int questID)
        {
            QuestCollection[questID].CompleteQuest();
        }

        public void ActivateQuest (int questID)
        {
            QuestCollection[questID].ActivateQuest();
        }

        public void DectivateQuest (int questID)
        {
            QuestCollection[questID].DectivateQuest();
        }

        public void FailQuest (int questID)
        {
            QuestCollection[questID].FailQuest();
        }
    }
}