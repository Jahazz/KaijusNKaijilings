using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScreenModel : BaseModel<BattleScreenView>
{
    private BattleActionResolver BattleActionResolver { get; set; } = new BattleActionResolver();
    private Battle CurrentBattle { get; set; }

    public void QueuePlayerSkillUsage (Entity caster, SkillScriptableObject skill)
    {
        CurrentBattle.GetPlayerBattleParticipant().QueueAttackAction(caster, CurrentBattle.GetOtherBattleParticipant().CurrentEntity.PresentValue, skill);
    }

    protected override void AttachToEvents ()
    {
        BattleFactory.OnBattleCreation += HandleOnBattleCreated;
    }

    private void HandleOnBattleCreated (Battle createdBattle)
    {
        CurrentBattle = createdBattle;

        foreach (BattleParticipant battleParticipant in CurrentBattle.BattleParticipantsCollection)
        {
            battleParticipant.CurrentEntity.OnVariableChange += (Entity entity) => OnPlayerEntityChanged(battleParticipant.Player, entity);
            OnPlayerEntityChanged(battleParticipant.Player, battleParticipant.CurrentEntity.PresentValue);
        }

        CurrentBattle.OnEntityDeath += HandleOnEntityDeath;
        CurrentBattle.CurrentBattleState.OnVariableChange += HandleOnCurrentBattleStateChange;

        CurrentView.IsBottomBarShown(true);
    }

    private void HandleOnCurrentBattleStateChange (BattleState newValue)
    {
        CurrentView.SetBottomUIBarInteractible(newValue == BattleState.ACTION_CHOOSE);

        if (newValue == BattleState.ACTION_RESOLVE)
        {
            ResolveAllActions(CurrentBattle.ResolveActionsQueue);
            //BattleActionResolver.StartResolvingActions(CurrentBattle.ResolveActionsQueue);
        }
    }
    private void ResolveAllActions (Queue<BaseBattleAction> baseBattleActionsCollection)
    {
        QueueHandler actionQueue = new QueueHandler(() => Debug.Log("NEXT PHASE"));
        while (baseBattleActionsCollection.Count > 0)
        {
            BaseBattleAction attackAction = baseBattleActionsCollection.Dequeue();

            if (attackAction != null)
            {
                ExecuteBattleParticipantAction(attackAction,actionQueue);
            }
        }
        actionQueue.InvokeActions();

    }

    public void ExecuteBattleParticipantAction (BaseBattleAction selectedAction, QueueHandler actionQueue)
    {
        switch (selectedAction.ActionType)
        {
            case BattleActionType.SWAP:
                break;
            case BattleActionType.ITEM:
                break;
            case BattleActionType.ATTACK:
                AttackBattleAction attackAction = selectedAction as AttackBattleAction;
                actionQueue.EnqueueFunction(() => CurrentView.PlayAnimationAsEntity(attackAction.Caster, AnimationType.ATTACK));
                actionQueue.EnqueueFunction(()=>ExecuteAttackAction(attackAction));
                actionQueue.EnqueueFunction(() => CurrentView.WaitUntilAnimatorIdle(attackAction.Caster));
                break;
            case BattleActionType.DISENGAGE:
                break;
            default:
                break;
        }
    }

    private IEnumerator ExecuteAttackAction (AttackBattleAction selectedAction)
    {
        CurrentBattle.ExecuteAttackAction(selectedAction);
        yield return null;
    }

    private void HandleOnEntityDeath (Entity entity)
    {
        CurrentView.PlayAnimationAsEntity(entity, AnimationType.DIE);
    }

    private void OnPlayerEntityChanged (Player owner, Entity newValue)
    {
        if (owner.IsNPC == false)
        {
            CurrentView.SpawnEntityInPlayerPosition(newValue, true);
            SetupSkills(newValue);
        }
        else
        {
            CurrentView.SpawnEntityInSecondPosition(newValue, false);
        }
    }

    private void SetupSkills (Entity entity)//TODO: add item skill
    {
        CurrentView.BindSkillToButtons(new List<SkillScriptableObject>(entity.SelectedSkillsCollection), entity);
    }

    protected override void DetachFromEvents ()
    {
        base.DetachFromEvents();
    }

}
