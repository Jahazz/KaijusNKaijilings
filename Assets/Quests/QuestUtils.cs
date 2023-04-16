using Ink.Runtime;
using QuantumTek.QuantumQuest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuestUtils
{
    public static void AddMethodsToStory (Story story)
    {
        story.BindExternalFunction(nameof(IsQuestStarted), (int questID) => { return IsQuestStarted(questID); });
        story.BindExternalFunction(nameof(StartQuest), (int questID) => StartQuest(questID));
        story.BindExternalFunction(nameof(ProgressQuestTask), (int questID, int taskID, int progress) => ProgressQuestTask(questID, taskID, progress));
        story.BindExternalFunction(nameof(IsQuestTaskFinished), (int x, int y) => { return IsQuestTaskFinished(x, y); });
    }

    private static bool IsQuestStarted (int questID)
    {
        return SingletonContainer.Instance.QuestHandler.QuestCollection.ContainsKey(questID);
    }

    private static void StartQuest (int questID)
    {
        SingletonContainer.Instance.QuestHandler.AssignQuest(questID);
    }

    private static void ProgressQuestTask (int questID, int taskID, float progress)
    {
        SingletonContainer.Instance.QuestHandler.ProgressTask(questID, taskID, progress);
    }

    private static bool IsQuestTaskFinished (int questID, int taskID)
    {
        QQ_Task task = SingletonContainer.Instance.QuestHandler.GetTask(questID, taskID);
        return task.Progress == task.MaxProgress;
    }
}
