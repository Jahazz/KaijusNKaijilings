using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScreenModel : BaseModel<BattleScreenView>
{
    private BattleActionResolver BattleActionResolver { get; set; }
    private Battle CurrentBattle { get; set; }

    public void QueuePlayerSkillUsage (Entity caster, SkillScriptableObject skill)
    {
        CurrentBattle.GetPlayerBattleParticipant().QueueAttackAction(caster, CurrentBattle.GetNPCBattleParticipant(), skill);
    }

    public void QueuePlayerEntitySwap (Entity entity)
    {
        CurrentBattle.GetPlayerBattleParticipant().QueueSwapAction(entity);
    }
    public bool IsInBattle ()
    {
        return CurrentBattle != null;
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
            StartCoroutine(ChangePlayerEntity(battleParticipant.Player, battleParticipant.CurrentEntity.PresentValue));
        }

        CurrentBattle.OnEntityDeath += HandleOnEntityDeath;
        CurrentBattle.CurrentBattleState.OnVariableChange += HandleOnCurrentBattleStateChange;
        CurrentBattle.OnAllActionsSelected += CurrentBattle_OnAllActionsSelected;

        CurrentView.IsBottomBarShown(true);
    }

    private void CurrentBattle_OnAllActionsSelected ()
    {
        InitializeBattleResolver();
        BattleActionResolver.SortActions(CurrentBattle.BattleParticipantsCollection);

        CurrentBattle.CurrentBattleState.PresentValue = BattleState.ACTION_RESOLVE;
    }

    private void HandleOnCurrentBattleStateChange (BattleState newValue)
    {
        CurrentView.SetBottomUIBarInteractible(newValue == BattleState.ACTION_CHOOSE);

        if (newValue == BattleState.ACTION_RESOLVE)
        {
            BattleActionResolver.ResolveAllActions();
        }
    }

    private void InitializeBattleResolver ()
    {
        BattleActionResolver = new BattleActionResolver(HandleOnAllActionsResolved);

        BattleActionResolver.OnAttackActionResolution += AddAttackActionToQueue;
        BattleActionResolver.OnDisengageActionResolution += AddDisengageActionToQueue;
        BattleActionResolver.OnItemActionResolution += AddItemActionToQueue;
        BattleActionResolver.OnSwapActionResolution += AddSwapActionToQueue;
    }

    private void HandleOnAllActionsResolved ()
    {
        DisposeOfBattleResolver();
        Debug.Log("NEXT PHASE");
    }

    private void DisposeOfBattleResolver ()
    {
        BattleActionResolver.OnAttackActionResolution -= AddAttackActionToQueue;
        BattleActionResolver.OnDisengageActionResolution -= AddDisengageActionToQueue;
        BattleActionResolver.OnItemActionResolution -= AddItemActionToQueue;
        BattleActionResolver.OnSwapActionResolution -= AddSwapActionToQueue;

        BattleActionResolver = null;
    }

    private void AddSwapActionToQueue (BaseBattleAction battleAction, QueueHandler actionQueue)
    {
        SwapBattleAction swapAction = battleAction as SwapBattleAction;

        actionQueue.EnqueueFunction(() => CurrentView.TryToDestroyEntityOnScene(swapAction.ActionOwner.CurrentEntity.PresentValue));
        actionQueue.EnqueueFunction(() => SwapEntityVariable(swapAction.ActionOwner, swapAction.EntityToSwapTo));
        actionQueue.EnqueueFunction(() => ChangePlayerEntity(swapAction.ActionOwner.Player, swapAction.EntityToSwapTo));
    }

    private void AddAttackActionToQueue (BaseBattleAction battleAction, QueueHandler actionQueue)
    {
        AttackBattleAction attackAction = battleAction as AttackBattleAction;

        actionQueue.EnqueueFunction(() => CurrentView.PlayAnimationAsEntity(attackAction.Caster, AnimationType.ATTACK));
        actionQueue.EnqueueFunction(() => ExecuteAttackAction(attackAction));
        actionQueue.EnqueueFunction(() => CurrentView.WaitUntilAnimatorIdle(attackAction.Caster));
    }

    private void AddDisengageActionToQueue (BaseBattleAction battleAction, QueueHandler actionQueue)
    {
        DisengageBattleAction disengageAction = battleAction as DisengageBattleAction;
    }

    private void AddItemActionToQueue (BaseBattleAction battleAction, QueueHandler actionQueue)
    {
        UseItemBattleAction useItemBattleAction = battleAction as UseItemBattleAction;
    }

    private IEnumerator SwapEntityVariable (BattleParticipant participant, Entity entityToSwapTo)
    {
        participant.CurrentEntity.PresentValue = entityToSwapTo;
        yield return null;
    }

    private IEnumerator ExecuteAttackAction (AttackBattleAction selectedAction)
    {
        CurrentBattle.ExecuteAttackAction(selectedAction);
        yield return null;
    }

    private void HandleOnEntityDeath (Entity entity, BattleParticipant entityOwner)
    {
        CurrentView.PlayAnimationAsEntity(entity, AnimationType.DIE);

        //reset action queue attacks for this entity
        //show entity chose screen if more than 0 entities in eq and player, if not player then just summon another
        //else show battle summary board 

    }

    private IEnumerator ChangePlayerEntity (Player owner, Entity newValue)
    {
        IEnumerator output;

        if (owner.IsNPC == false)
        {
            output = CurrentView.SpawnEntityInPlayerPosition(newValue, true);
            SetupSkills(newValue);
        }
        else
        {
            output = CurrentView.SpawnEntityInSecondPosition(newValue, false);
        }

        return output;
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
