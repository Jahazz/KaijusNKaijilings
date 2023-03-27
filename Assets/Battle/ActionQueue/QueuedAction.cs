using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueuedAction
{
    public delegate void ActionFinishedParams ();
    public event ActionFinishedParams OnActionFinished;

    public Func<IEnumerator> ActionToExecute { get; set; }
    public QueuedAction (Func<IEnumerator> actionToExecute)
    {
        ActionToExecute = actionToExecute;
    }

    public void Execute ()
    {
        SingletonContainer.Instance.StartCoroutine(ExecuteIEnumerator());
    }

    private IEnumerator ExecuteIEnumerator ()
    {
        yield return ActionToExecute.Invoke();
        OnActionFinished?.Invoke();
    }
}
