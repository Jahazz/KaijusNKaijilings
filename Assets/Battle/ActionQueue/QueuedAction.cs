using BattleCore.Actions;
using System;
using System.Collections;

namespace BattleCore.ActionQueue
{
    public class QueuedAction
    {
        public delegate void ActionFinishedParams ();
        public event ActionFinishedParams OnActionFinished;

        public Func<IEnumerator> ActionToExecute { get; set; }
        public BaseBattleAction ActionIsBasedOn { get; private set; }

        public QueuedAction (Func<IEnumerator> actionToExecute, BaseBattleAction actionIsBasedOn)
        {
            ActionToExecute = actionToExecute;
            ActionIsBasedOn = actionIsBasedOn;
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
}