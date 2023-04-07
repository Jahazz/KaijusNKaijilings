using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class QueueHandler
{
    private Queue<QueuedAction> QueuedActionCollection { get; set; } = new Queue<QueuedAction>();
    private Action FinishCallback { get; set; }

    public QueueHandler (Action finishCallback)
    {
        FinishCallback = finishCallback;
    }

    public void EnqueueFunction (BaseBattleAction basedOnAction, Func<IEnumerator> functionToExecute)
    {
        QueuedActionCollection.Enqueue(new QueuedAction(functionToExecute, basedOnAction));
    }

    public void InvokeActions ()
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

    public void RemoveActionsWithPredicate (Func<QueuedAction, bool> predicate)
    {
        QueuedActionCollection = new Queue<QueuedAction>(QueuedActionCollection.Where((n) => predicate(n) == false));
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
