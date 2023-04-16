using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Interlocutor : MonoBehaviour
{
    [field: SerializeField]
    public Player AssignedPlayer { get; private set; }
    [field: SerializeField]
    public TextAsset InkStoryFile { get; private set; }
    [field: SerializeField]
    public List<Interlocutor> AdditionalInterlocutors { get; set; }
    [field: SerializeField]
    public List<UnityEventWithNamePair> EventsCollection { get; set; }

    public void InitializeConversation ()
    {
        SingletonContainer.Instance.DialogueManager.Initialize(SingletonContainer.Instance.PlayerManager.CurrentPlayer, this);
    }

    public UnityEvent GetFunctionByName (string functionName)
    {
        return EventsCollection.Where(x => x.EventName == functionName).FirstOrDefault().AssignedEvent;
    }

    public Story GenerateStory ()
    {
        Story story = new Story(InkStoryFile.text);

        foreach (UnityEventWithNamePair item in EventsCollection)
        {
            story.BindExternalFunction(item.EventName, ()=> item.AssignedEvent.Invoke());
        }

        QuestUtils.AddMethodsToStory(story);

        return story;
    }
}
