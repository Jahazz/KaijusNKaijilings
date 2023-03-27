using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleActionResolver
{
    public void StartResolvingActions (Queue<BaseBattleAction> baseBattleActionsCollection)
    {
        SingletonContainer.Instance.StartCoroutine(ResolveAllActions(baseBattleActionsCollection));
    }

    private IEnumerator ResolveAllActions (Queue<BaseBattleAction> baseBattleActionsCollection)
    {
        while (baseBattleActionsCollection.Count > 0)
        {
            AttackBattleAction attackAction = baseBattleActionsCollection.Dequeue() as AttackBattleAction;

            if (attackAction != null)
            {
                yield return new WaitForSeconds(ResolveOneAction(attackAction));
            }
        }
            
    }

    private float ResolveOneAction (AttackBattleAction attackAction)
    {
        return 3;
        //CurrentView.PlayAnimationAsEntity(attackAction.Target, AnimationType.GET_HIT);
        //return CurrentView.PlayAnimationAsEntity(attackAction.Caster, AnimationType.ATTACK);
    }
}
