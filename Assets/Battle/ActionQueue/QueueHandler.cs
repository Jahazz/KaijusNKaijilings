using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueHandler
{
    private Queue<QueuedAction> QueuedActionCollection { get; set; } = new Queue<QueuedAction>();
    private Action FinishCallback { get; set; }

    public QueueHandler (Action finishCallback)
    {
        FinishCallback = finishCallback;
    }

    public void EnqueueFunction (Func<IEnumerator> functionToExecute)
    {
        QueuedActionCollection.Enqueue(new QueuedAction(functionToExecute));
    }

    public void InvokeActions ()
    {
        ExecuteNext();
    }

    private void ExecuteNext ()
    {
        QueuedAction action = QueuedActionCollection.Dequeue();
        action.OnActionFinished += HandleOnSingleActionFinished;
        action.Execute();
    }

    private void HandleOnSingleActionFinished ()
    {
        if (QueuedActionCollection.Count > 0)
        {
            ExecuteNext();
        }
        else
        {
            FinishCallback.Invoke();
        }
    }
}
